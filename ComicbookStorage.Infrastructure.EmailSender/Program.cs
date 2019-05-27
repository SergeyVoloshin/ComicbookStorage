
namespace ComicbookStorage.Infrastructure.EmailSender
{
    using Domain.Core.Entities;
    using Domain.Core.Entities.Specifications.Email;
    using Domain.DataAccess;
    using Domain.DataAccess.Repositories;
    using EF;
    using EF.Repositories;
    using MailKit.Net.Smtp;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MimeKit;
    using System;
    using System.Threading.Tasks;

    internal class Program
    {
        private static IServiceProvider serviceProvider;

        private static async Task Main(string[] args)
        {
            RegisterServices();
            try
            {
                var emailRepository = serviceProvider.GetService<IEmailRepository>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
                var settings = serviceProvider.GetService<IEmailSenderSettings>();

                var emails = await emailRepository.GetTopAsync(new NotSentEmailSpec(settings.ResendIntervalMinutes), settings.MaxEmailCount);

                foreach (var email in emails)
                {
                    emailRepository.Update(email);
                    email.SetSendingStatus();
                }

                // Update statuses just to make sure that all the emails will be sent only once
                // This can happen if two or more email senders are started at the same time
                await unitOfWork.SaveAsync();

                foreach (var email in emails)
                {
                    try
                    {
                        await SendEmailAsync(email);
                        email.SetSentStatus();
                    }
                    catch (Exception e)
                    {
                        email.SetErrorStatus(e.Message);
                    }
                }
                await unitOfWork.SaveAsync();
            }
            finally
            {
                DisposeServices();
            }
        }

        private static async Task SendEmailAsync(Email email)
        {
            var settings = serviceProvider.GetService<IEmailSenderSettings>();
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(settings.SenderName, settings.Sender));
            mimeMessage.To.Add(new MailboxAddress(email.Recipient));
            mimeMessage.Subject = email.Subject;
            mimeMessage.Body = new TextPart("html")
            {
                Text = email.Body
            };

            using (SmtpClient client = new SmtpClient())
            {
                await client.ConnectAsync(settings.MailServer, settings.MailPort, false);
                await client.AuthenticateAsync(settings.Sender, settings.Password);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }

        private static void RegisterServices()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<ComicbookStorageContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            EmailSenderSettings settings = new EmailSenderSettings();
            config.GetSection("EmailSenderSettings").Bind(settings);
            services.AddSingleton<IEmailSenderSettings>(settings);


            serviceProvider = services.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (serviceProvider is IDisposable provider)
            {
                provider.Dispose();
            }
        }

    }
}
