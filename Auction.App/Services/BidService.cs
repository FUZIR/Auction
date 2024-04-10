using Auction.App.Interfaces;
using Auction.DataAccess.Postgres.Entities;

namespace Auction.App.Services;

public class BidService(IBidRepository bidRepository) : IBidService, IBidRepository
{
    public async Task<Guid> CreateBid(Guid id, Guid lotId, Guid userId, decimal bid, DateTime timestamp)
    {
        return await bidRepository.CreateBid(id, lotId, userId, bid, timestamp);
    }

    public async Task<List<BidEntity>> GetUserBids(Guid userId)
    {
        return await bidRepository.GetUserBids(userId);
    }
}