
namespace ComicbookStorage.Infrastructure.EmailSender
{
    using Domain.DataAccess;
    using Domain.DataAccess.Repositories;
    using EF;
    using EF.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
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
                var sender = serviceProvider.GetService<IEmailSender>();
                await sender.AllocateEmails();
                await sender.SendAll();
            }
            finally
            {
                DisposeServices();
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
            services.AddScoped<IEmailSender, EmailSender>();

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
