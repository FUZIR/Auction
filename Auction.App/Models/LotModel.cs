using Auction.App.Services;
using Auction.DataAccess.Postgres;
using Auction.DataAccess.Postgres.Entities;
using Auction.DataAccess.Postgres.Enums;
using Microsoft.EntityFrameworkCore;

namespace Auction.App.Models;

public class LotModel
{
    private readonly AuctionDbContext _dbContext;
    private LotModel(Guid id, string name, string description, decimal startingPrice, decimal? currentPrice, decimal? buyPrice,
        DateTime startTime, DateTime endTime, Status status, Guid creatorId, Guid? buyerId, AuctionDbContext dbContext)
    {
        Id = id;
        Name = name;
        Description = description;
        StartingPrice = startingPrice;
        CurrentPrice = currentPrice;
        BuyPrice = buyPrice;
        StartTime = startTime;
        EndTime = endTime;
        Status = status;
        CreatorId = creatorId;
        BuyerId = buyerId;
        _dbContext = dbContext;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal StartingPrice { get; set; }
    public decimal? CurrentPrice { get; set;}
    public decimal? BuyPrice { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Status Status { get; set; }
    public Guid CreatorId { get; set; }
    public Guid? BuyerId { get; set; }

    public static async Task<(LotModel lot, string error)> Create(Guid id, string name, string description, decimal startingPrice,
        decimal? currentPrice, decimal? buyPrice,
        DateTime startTime, DateTime endTime, Status status, Guid creatorId, Guid? buyerId,AuctionDbContext dbContext)
    {
        var error = string.Empty;
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == creatorId);
        if (user == null)
        {
            error = "User with such id not found";
            return (null, error);
        }
        else if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
        {
            error = "Name or description cannot be empty";
            return (null, error);
        }
        else if (startingPrice <= 0 || buyPrice <= 0  )
        {
            error = "Starting price must be greater than zero";
        }
        else if (buyPrice < startingPrice)
        {
            error = "Buy price cannot be less than starting price";
            return (null, error);
        }
        var lot = new LotModel(id, name, description, startingPrice, currentPrice, buyPrice,startTime, endTime, status, creatorId, buyerId, dbContext);
        return (lot, error);
    }

    public static async Task<(Guid id, string error)> Delete(Guid id, AuctionDbContext dbContext)
    {
        var error = string.Empty;
        var lot = await dbContext.Lots.FirstOrDefaultAsync(l => l.Id == id);
        
        if (lot == null)
        {
            error = "Lot with such id not found";
            return (Guid.Empty, error);
        }

        return (lot.Id, error);

    }

    public static async Task<(LotEntity lot, string error)> Get(Guid id, AuctionDbContext dbContext)
    {
        var error = string.Empty;
        var lot = await dbContext.Lots.FirstOrDefaultAsync(l => l.Id == id);

        if (lot == null)
        {
            error = "Lot with such id not found";
            return (null, error);
        }

        return (lot, error);
    }

    public static async Task<(List<LotEntity>, string error)> GetAll(AuctionDbContext dbContext)
    {
        var error = string.Empty;
        var lots = await dbContext.Lots.ToListAsync();

        if (lots == null)
        {
            error = "Lots not found";
            return (null, error);
        }

        return (lots, error);
    }

    public static async Task<(List<LotEntity>, string error)> GetAllUserLots(Guid userId, AuctionDbContext dbContext)
    {
        var error = string.Empty;
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            error = "User with such id not found";
            return (null, error);
        }
        var lots = await dbContext.Lots.Where(l => l.CreatorId == userId).ToListAsync();
        return (lots, error);
    }
}