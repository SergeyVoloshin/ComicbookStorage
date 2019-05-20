
namespace ComicbookStorage.Domain.DataAccess.Repositories
{
    using System.Threading.Tasks;
    using Base;
    using Core.Entities;

    public interface IEmailTemplateRepository : IRepository<EmailTemplate>
    {
        Task<EmailTemplate> GetAsync(EmailTemplateId id);
    }
}
