
namespace ComicbookStorage.Infrastructure.EF.Repositories
{
    using Base;
    using Domain.Core.Entities;
    using Domain.DataAccess.Repositories;
    using System.Threading.Tasks;

    public class EmailTemplateRepository : RepositoryBase<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(ComicbookStorageContext context) : base(context)
        {
        }

        public Task<EmailTemplate> GetAsync(EmailTemplateId id)
        {
            return GetAsync((int)id);
        }
    }
}
