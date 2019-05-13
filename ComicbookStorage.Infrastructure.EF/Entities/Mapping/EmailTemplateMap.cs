﻿
namespace ComicbookStorage.Infrastructure.EF.Entities.Mapping
{
    using Base;
    using Domain.Core.Entities;
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
        }
    }
}