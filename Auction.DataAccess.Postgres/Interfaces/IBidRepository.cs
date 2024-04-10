using Auction.DataAccess.Postgres.Entities;

namespace Auction.App.Interfaces;

public interface IBidRepository
{
    public Task<Guid> CreateBid(Guid id, Guid lotId, Guid userId, decimal bid, DateTime timestamp);

    public Task<List<BidEntity>> GetUserBids(Guid userId);
}