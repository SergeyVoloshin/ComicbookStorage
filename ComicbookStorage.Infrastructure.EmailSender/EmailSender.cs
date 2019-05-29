
namespace ComicbookStorage.Infrastructure.EmailSender
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Core.Entities;
    using Domain.Core.Entities.Specifications.Email;
    using Domain.DataAccess;
    using Domain.DataAccess.Repositories;
    using MailKit.Net.Smtp;
    using Microsoft.EntityFrameworkCore;
    using MimeKit;

    internal interface IEmailSender
    {
        IReadOnlyList<Email> Emails { get; }

        Task AllocateEmails();

        Task SendAll();
    }

    internal class EmailSender : IEmailSender
    {
        private readonly IEmailRepository emailRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEmailSenderSettings settings;
        private readonly List<Email> emails;

        public EmailSender(
            IEmailRepository emailRepository,
            IUnitOfWork unitOfWork,
            IEmailSenderSettings settings)
        {
            this.emailRepository = emailRepository;
            this.unitOfWork = unitOfWork;
            this.settings = settings;
            emails = new List<Email>();
        }

        public IReadOnlyList<Email> Emails => emails.AsReadOnly();

        public async Task AllocateEmails()
        {
            var fetchedEmails = await emailRepository.GetTopAsync(new NotSentEmailSpec(settings.ResendIntervalMinutes), settings.MaxEmailCount);

            foreach (var email in fetchedEmails)
            {
                // Update statuses just to make sure that all fetched emails are sent only once
                // A collision can happen if two or more email senders are started at the same time
                // In case of a collision we get DbUpdateConcurrencyException
                emailRepository.Update(email);
                email.SetSendingStatus();
                try
                {
                    await unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    continue;
                }
                emails.Add(email);
            }
        }

        public async Task SendAll()
        {
            if (Emails.Count > 0)
            {
                foreach (var email in Emails)
                {
                    try
                    {
                        await SendEmail(email);
                        email.SetSentStatus();
                    }
                    catch (Exception e)
                    {
                        email.SetErrorStatus(e.Message);
                    }
                }

                await unitOfWork.SaveAsync();
                emails.Clear();
            }
        }

        protected async Task SendEmail(Email email)
        {
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
    }
}
