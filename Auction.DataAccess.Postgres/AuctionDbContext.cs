using Auction.DataAccess.Postgres.Configurations;
using Auction.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.DataAccess.Postgres
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<LotEntity> Lots { get; set; }
        public DbSet<BidEntity> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LotConfiguration());
            modelBuilder.ApplyConfiguration(new BidConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
