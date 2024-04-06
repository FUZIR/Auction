using Auction.DataAccess.Postgres.Entities;

namespace Auction.App.Interfaces;

public interface IBidService
{
    Task<Guid> CreateBid(Guid lotId, Guid userId, decimal bid);
    Task<List<BidEntity>> GetUserBids(Guid userId);
}