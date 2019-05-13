
namespace ComicbookStorage.Infrastructure.EF.Entities.Mapping
{
    using Base;
    using Domain.Core.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ComicbookMap : EntityMap<Comicbook>
    {
        public override void Configure(EntityTypeBuilder<Comicbook> builder)
        {
            base.Configure(builder);

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
