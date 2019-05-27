
namespace ComicbookStorage.Domain.DataAccess.Repositories
{
    using System.Threading.Tasks;
    using Base;
    using Core.Entities;

    public interface IEmailRepository : IRepository<Email>
    {
        Task<EmailTemplate> GetEmailTemplateAsync(EmailTemplateId id);
    }
}
