
namespace ComicbookStorage.Infrastructure.EF
{
    using Domain.Core.Entities;
    using Entities.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class ComicbookStorageContext : DbContext
    {
        public ComicbookStorageContext(DbContextOptions<ComicbookStorageContext> options) : base(options)
        {
        }

        public DbSet<Comicbook> Comicbooks { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComicbookMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new EmailTemplateMap());
            modelBuilder.ApplyConfiguration(new EmailMap());

            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!entityType.IsOwned())
                {
                    entityType.Relational().TableName = entityType.DisplayName();
                }
            }
        }
    }
}
