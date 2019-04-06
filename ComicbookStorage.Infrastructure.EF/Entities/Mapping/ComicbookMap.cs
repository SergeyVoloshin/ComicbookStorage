
namespace ComicbookStorage.Infrastructure.EF.Entities.Mapping
{
    using Domain.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ComicbookMap : IEntityTypeConfiguration<Comicbook>
    {
        public void Configure(EntityTypeBuilder<Comicbook> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
