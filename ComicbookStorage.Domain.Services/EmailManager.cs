
namespace ComicbookStorage.Domain.Services
{
    using Base;
    using Core.Entities;
    using DataAccess;
    using DataAccess.Repositories;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmailManager : IManager
    {
        Task EnqueueEmailConfirmation(User user, string applicationName, string confirmationUrl);
    }

    public class EmailManager : ManagerBase, IEmailManager
    {
        private const string ApplicationNameKey = "{ApplicationName}";
        private const string ConfirmationLinkKey = "{ConfirmationLink}";

        private readonly IEmailRepository emailRepository;

        public EmailManager(
            IEmailRepository emailRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.emailRepository = emailRepository;
        }

        public async Task EnqueueEmailConfirmation(User user, string applicationName, string confirmationUrl)
        {
            var fields = new Dictionary<string, string>()
            {
                {ApplicationNameKey, applicationName},
                {ConfirmationLinkKey, confirmationUrl}
            };
            var template = await emailRepository.GetEmailTemplateAsync(EmailTemplateId.EmailConfirmation);
            Email email = template.CreateEmail(user.Email, fields, user);
            emailRepository.Add(email);
            await UnitOfWork.SaveAsync();
        }
    }
}
