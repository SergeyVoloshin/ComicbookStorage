
namespace ComicbookStorage.Infrastructure.EF.Entities.Mapping
{
    using Domain.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

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
                .HasMaxLength(36);
        }
    }
}
