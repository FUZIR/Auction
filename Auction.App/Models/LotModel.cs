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
}