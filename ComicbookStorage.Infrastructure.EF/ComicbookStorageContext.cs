
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.Relational().TableName = entityType.DisplayName();
            }

            modelBuilder.ApplyConfiguration(new ComicbookMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new EmailTemplateMap());
        }
    }
}
