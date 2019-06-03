
namespace ComicbookStorage.Infrastructure.EF.Entities.Mapping
{
    using Base;
    using Domain.Core.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserMap : EntityMap<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasAlternateKey(e => e.Name);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);
            builder.HasAlternateKey(e => e.Email);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Salt)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.EncryptionIterationCount)
                .IsRequired();

            builder.Property(e => e.ConfirmationCode)
                .HasMaxLength(255);

            builder.Property(e => e.IsEmailConfirmed)
                .IsRequired();
        }
    }
}
