using Auction.DataAccess.Postgres.Entities;
using Auction.DataAccess.Postgres.Enums;
using Microsoft.EntityFrameworkCore;

namespace Auction.DataAccess.Postgres.Repositories;

public class LotRepository
{
    private readonly AuctionDbContext _dbContext;

    public LotRepository(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Create(Guid id, string name, string description, decimal startprice, decimal buyprice, Guid creatorId,
        Guid buyerId)
    {
        var lot = new LotEntity()
        {
            Id = id,
            Name = name,
            Description = description,
            StartingPrice = startprice,
            CurrentPrice = null,
            BuyPrice = buyprice,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddDays(1),
            Status = Status.ACTIVE,
            CreatorId = creatorId,
            BuyerId = buyerId
        };

        await _dbContext.Lots.AddAsync(lot);
        await _dbContext.SaveChangesAsync();

        return lot.Id;
    }

    public async Task Delete(Guid id)
    {
        await _dbContext.Lots.Where(l => l.Id == id).ExecuteDeleteAsync();
    }

    public async Task<LotEntity> Get(Guid id)
    {
        return await _dbContext.Lots.FirstOrDefaultAsync(l => l.Id == id)?? throw new Exception();
    }
    
    
    public async Task<IEnumerable<LotEntity>> GetAll()
    {
        return await _dbContext.Lots.ToListAsync();
    }

    public async Task<IEnumerable<LotEntity>> GetAllUserLots(Guid userId)
    {
        return await _dbContext.Lots.Where(l => l.CreatorId == userId).ToListAsync();
    }
}