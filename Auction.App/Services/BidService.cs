using Auction.App.Interfaces;
using Auction.DataAccess.Postgres.Entities;

namespace Auction.App.Services;

public class BidService(IBidRepository bidRepository) : IBidService, IBidRepository
{
    public async Task<Guid> CreateBid(Guid lotId, Guid userId, decimal bid)
    {
        return await bidRepository.CreateBid(lotId, userId, bid);
    }

    public async Task<List<BidEntity>> GetUserBids(Guid userId)
    {
        return await bidRepository.GetUserBids(userId);
    }
}