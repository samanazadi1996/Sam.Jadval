﻿using Jadval.Domain.OutBoxEventItems.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jadval.Infrastructure.Persistence.Contexts.Configurations
{
    public class OutBoxEventItemConfiguration : IEntityTypeConfiguration<OutBoxEventItem>
    {

        public void Configure(EntityTypeBuilder<OutBoxEventItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.EventName).HasMaxLength(100);
            builder.Property(p => p.EventTypeName).HasMaxLength(300);
            builder.Property(p => p.EventPayload).HasMaxLength(1000);
        }
    }
}
