
namespace ComicbookStorage.Infrastructure.EF.Repositories
{
    using Base;
    using Domain.Core.Entities;
    using Domain.DataAccess.Repositories;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class EmailRepository : RepositoryBase<Email>, IEmailRepository
    {
        public EmailRepository(ComicbookStorageContext context) : base(context)
        {
        }

        public Task<EmailTemplate> GetEmailTemplateAsync(EmailTemplateId id)
        {
            return Context.EmailTemplates.FirstAsync(e => e.Id == (int)id);
        }
    }
}
