﻿using Auction.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.DataAccess.Postgres.Configurations
{
    internal class LotConfiguration : IEntityTypeConfiguration<LotEntity>
    {
        public void Configure(EntityTypeBuilder<LotEntity> builder)
        {
            builder.HasKey(l=>l.Id);

            builder
                .HasOne(l => l.Creator)
                .WithMany(u=>u.CreatedLots)
                .HasForeignKey(l=>l.CreatorId)
                .IsRequired();

            builder
                .HasOne(l => l.Buyer)
                .WithMany(u => u.BoughtLots)
                .HasForeignKey(l => l.BuyerId);

            builder.Property(l=>l.Name).IsRequired();
            builder.Property(l => l.Description).IsRequired();
            builder.Property(l => l.StartTime).IsRequired();
            builder.Property(l => l.EndTime).IsRequired();
            builder.Property(l => l.StartingPrice).IsRequired();
            builder.Property(l=>l.CurrentPrice).IsRequired(false);
            builder.Property(l=>l.BuyPrice).IsRequired(false);
            builder.Property(l=>l.BuyerId).IsRequired(false);

        }
    }
}
