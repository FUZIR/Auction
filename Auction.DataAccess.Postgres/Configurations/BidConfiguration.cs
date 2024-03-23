using Auction.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.DataAccess.Postgres.Configurations
{
    internal class BidConfiguration : IEntityTypeConfiguration<BidEntity>
    {
        public void Configure(EntityTypeBuilder<BidEntity> builder)
        {
            builder.HasKey(b => b.Id);

            builder
                .HasOne(u => u.User)
                .WithMany(u => u.Bids)
                .HasForeignKey(u => u.UserId);

            builder.Property(b => b.LotId).IsRequired();
            builder.Property(b=>b.UserId).IsRequired();
            builder.Property(b => b.Bid).IsRequired();
        }
    }
}
