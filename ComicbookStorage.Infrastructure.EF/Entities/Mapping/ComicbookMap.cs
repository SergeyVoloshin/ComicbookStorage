
namespace ComicbookStorage.Infrastructure.EF.Entities.Mapping
{
    using Domain.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ComicbookMap : IEntityTypeConfiguration<Comicbook>
    {
        public void Configure(EntityTypeBuilder<Comicbook> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.CoverExtension)
                .IsRequired()
                .HasMaxLength(4);

            builder.HasAlternateKey(e => e.SeoUrl);
            builder.Property(e => e.SeoUrl)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .HasMaxLength(1024);
        }
    }
}
