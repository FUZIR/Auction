using Auction.DataAccess.Postgres.Entities;

namespace Auction.DataAccess.Postgres.Interfaces;

public interface ILotRepository
{
    public Task<Guid> Create(Guid id, string name, string description, decimal startprice, decimal? buyprice,
        Guid creatorId,
        Guid? buyerId);

    public Task Delete(Guid id);

    public Task<LotEntity> Get(Guid id);

    public Task<IEnumerable<LotEntity>> GetAll();

    public Task<IEnumerable<LotEntity>> GetAllUserLots(Guid userId);
}