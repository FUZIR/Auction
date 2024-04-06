using Auction.DataAccess.Postgres.Entities;

namespace Auction.App.Interfaces;

public interface ILotService
{
    Task<Guid> Create(Guid id, string name, string description, decimal startprice, decimal buyprice, Guid creatorId,
        Guid buyerId);

    Task Delete(Guid id);
    Task<LotEntity> Get(Guid id);
    Task<IEnumerable<LotEntity>> GetAll();
    Task<IEnumerable<LotEntity>> GetAllUserLots(Guid userId);
}