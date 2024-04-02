using Auction.Application.Enums;
using Auction.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.DataAccess.Postgres.Repositories;

public class LotRepository
{
    private readonly AuctionDbContext _dbContext;

    public LotRepository(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateLot(Guid id, string name, string description, decimal startprice, decimal buyprice, Guid creatorId,
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

    public async Task DeleteLot(Guid id)
    {
        var Lot = await _dbContext.Lots.FirstOrDefaultAsync(l => l.Id == id);
        _dbContext.Remove(Lot);
        _dbContext.SaveChangesAsync();
    }

    public async Task<LotEntity> GetLot(Guid id)
    {
        return await _dbContext.Lots.FirstOrDefaultAsync(l => l.Id == id);
    }
    
    public async Task<IEnumerable<LotEntity>> GetAllLots()
    {
        return await _dbContext.Lots.ToListAsync();
    }

    public async Task<IEnumerable<LotEntity>> GetAllUserLots(Guid userId)
    {
        return await _dbContext.Lots.Where(l => l.CreatorId == userId).ToListAsync();
    }
}