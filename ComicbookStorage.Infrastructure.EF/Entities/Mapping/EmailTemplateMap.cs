
namespace ComicbookStorage.Infrastructure.EF.Entities.Mapping
{
    using Base;
    using Domain.Core.Entities;
    using Localization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class EmailTemplateMap : EntityMap<EmailTemplate>
    {
        public override void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Body)
                .IsRequired();

            builder.HasMany(e => e.Emails)
                .WithOne();

            builder.Metadata.FindNavigation(nameof(EmailTemplate.Emails))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasData(new EmailTemplate(EmailTemplateId.EmailConfirmation, LocalizedResources.EmailConfirmationSubject, LocalizedResources.EmailConfirmationMessage));
        }
    }
}
