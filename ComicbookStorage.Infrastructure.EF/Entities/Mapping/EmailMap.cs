
namespace ComicbookStorage.Infrastructure.EF.Entities.Mapping
{
    using Base;
    using Domain.Core.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class EmailMap : EntityMap<Email>
    {
        public override void Configure(EntityTypeBuilder<Email> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Recipient)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Body)
                .IsRequired();

            builder.Property(e => e.CreationTime)
                .IsRequired();
        }
    }
}
