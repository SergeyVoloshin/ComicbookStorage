
namespace ComicbookStorage.Domain.Services
{
    using Base;
    using Core.Entities;
    using DataAccess;
    using DataAccess.Repositories;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Entities.Base;
    using Infrastructure.Localization;

    public interface IEmailManager : IManager
    {
        Task EnqueueEmailConfirmation(User user, string confirmationUrl);
        Task EnqueuePasswordResetEmail(User user, string newPassword, string logInUrl);
    }

    public class EmailManager : ManagerBase, IEmailManager
    {
        private const string ApplicationNameKey = "{ApplicationName}";
        private const string ConfirmationLinkKey = "{ConfirmationLink}";
        private const string LogInLinkKey = "{LogInLink}";
        private const string NewPasswordKey = "{NewPassword}";

        private readonly IEmailRepository emailRepository;

        public EmailManager(
            IEmailRepository emailRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.emailRepository = emailRepository;
        }

        public Task EnqueueEmailConfirmation(User user, string confirmationUrl)
        {
            var fields = new Dictionary<string, string>
            {
                {ApplicationNameKey, LocalizedResources.ApplicationName},
                {ConfirmationLinkKey, confirmationUrl}
            };
            return EnqueueEmail(EmailTemplateId.EmailConfirmation, user.Email, fields, user);
        }

        public Task EnqueuePasswordResetEmail(User user, string newPassword, string logInUrl)
        {
            var fields = new Dictionary<string, string>
            {
                {ApplicationNameKey, LocalizedResources.ApplicationName},
                {LogInLinkKey, logInUrl},
                {NewPasswordKey, newPassword}
            };
            return EnqueueEmail(EmailTemplateId.PasswordReset, user.Email, fields, user);
        }

        private async Task EnqueueEmail(EmailTemplateId id, string recipient, IReadOnlyDictionary<string, string> fields, params IEntity[] entities)
        {
            var template = await emailRepository.GetEmailTemplateAsync(id);
            Email email = template.CreateEmail(recipient, fields, entities);
            emailRepository.Add(email);
            await UnitOfWork.SaveAsync();
        }
    }
}
