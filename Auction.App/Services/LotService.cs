using Auction.App.Interfaces;
using Auction.DataAccess.Postgres;
using Auction.DataAccess.Postgres.Entities;
using Auction.DataAccess.Postgres.Interfaces;

namespace Auction.App.Services;

public class LotService(ILotRepository lotRepository) : ILotService
{
    public async Task<Guid> Create(Guid id, string name, string description, decimal startprice, decimal? buyprice, Guid creatorId,
        Guid? buyerId)
    {
        return await lotRepository.Create(id, name, description, startprice, buyprice, creatorId, buyerId);
    }

    public async Task Delete(Guid id)
    {
        await lotRepository.Delete(id);
    }

    public async Task<LotEntity> Get(Guid id)
    {
        return await lotRepository.Get(id);
    }

    public async Task<IEnumerable<LotEntity>> GetAll()
    {
        return await lotRepository.GetAll();
    }

    public async Task<IEnumerable<LotEntity>> GetAllUserLots(Guid userId)
    {
        return await lotRepository.GetAllUserLots(userId);
    }

    
}