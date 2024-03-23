using Auction.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.DataAccess.Postgres.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .HasMany(u => u.CreatedLots)
                .WithOne(l => l.Creator)
                .HasForeignKey(l=>l.CreatorId);
            builder
                .HasMany(u => u.BoughtLots)
                .WithOne(l => l.Buyer)
                .HasForeignKey(l => l.BuyerId);

            builder.Property(u=>u.Email).IsRequired().HasMaxLength(100); 
            builder.Property(u=>u.Password).IsRequired();
        }
    }
}
