using Auction.DataAccess.Postgres.Entities;

namespace Auction.App.Interfaces;

public interface IBidService
{
    Task<Guid> CreateBid(Guid id, Guid lotId, Guid userId, decimal bid, DateTime timestamp);
    Task<List<BidEntity>> GetUserBids(Guid userId);
}