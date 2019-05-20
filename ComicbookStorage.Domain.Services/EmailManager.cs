
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

        private readonly IEmailTemplateRepository emailTemplateRepository;

        public EmailManager(
            IEmailTemplateRepository emailTemplateRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.emailTemplateRepository = emailTemplateRepository;
        }

        public async Task EnqueueEmailConfirmation(User user, string applicationName, string confirmationUrl)
        {
            var fields = new Dictionary<string, string>()
            {
                {ApplicationNameKey, applicationName},
                {ConfirmationLinkKey, confirmationUrl}
            };
            var template = await emailTemplateRepository.GetAsync((int)EmailTemplateId.EmailConfirmation);
            template.AddEmail(user.Email, fields, user);
            emailTemplateRepository.AddGraph(template);
            await UnitOfWork.SaveAsync();
        }
    }
}
