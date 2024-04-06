using Auction.DataAccess.Postgres.Entities;

namespace Auction.App.Interfaces;

public interface IBidRepository
{
    public Task<Guid> CreateBid(Guid lotId, Guid userId, decimal bid);

    public Task<List<BidEntity>> GetUserBids(Guid userId);
}