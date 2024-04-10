using Auction.App.Interfaces;
using Auction.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.DataAccess.Postgres.Repositories;

public class BidRepository : IBidRepository
{
    private readonly AuctionDbContext _dbContext;

    public BidRepository(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateBid(Guid id,Guid lotId, Guid userId, decimal bid, DateTime timestamp)
    {
        var lotBid = new BidEntity()
        {
            Id = id,
            LotId = lotId,
            UserId = userId,
            Bid = bid,
            TimeStamp = timestamp
        };
        await _dbContext.Bids.AddAsync(lotBid);
        await _dbContext.SaveChangesAsync();

        return lotBid.Id;
    }

    public async Task<List<BidEntity>> GetUserBids(Guid userId)
    {
        return await _dbContext.Bids.Where(b => b.UserId == userId).ToListAsync();
    }
}