using Auction.DataAccess.Postgres;
using Auction.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.App.Models;

public class BidModel
{
    private readonly AuctionDbContext _dbContext;
    private BidModel(Guid id, Guid lotId, Guid userId, decimal bid, DateTime timeStamp, AuctionDbContext dbContext)
    {
        Id = id;
        LotId = lotId;
        UserId = userId;
        Bid = bid;
        TimeStamp = timeStamp;
        _dbContext = dbContext;
    }
    public Guid Id { get; set; }
    public Guid LotId { get; set; }
    public Guid UserId { get; set; }
    public decimal Bid {  get; set; }
    public DateTime TimeStamp { get; set; }

    public static async Task<(BidModel bidModel, string error)> Create(Guid id, Guid lotId, Guid userId, decimal bid, DateTime timeStamp, AuctionDbContext dbContext)
    {
        var error = string.Empty;
        var lot = await dbContext.Lots.FirstOrDefaultAsync(l => l.Id == lotId);
        if (lot == null)
        {
            error = "Lot with such id not found";
            return (null, error);
        }
        else if (lot.CurrentPrice > bid || bid < lot.StartingPrice)
        {
            error = "Invalid bid";
            return (null, error);
        }
        else if (lot.EndTime < timeStamp)
        {
            error = "Lot has ended";
            return (null, error);
        } 
        var newBid = new BidModel(id, lotId, userId, bid, timeStamp, dbContext);
        await UpdateCurrentLot(lotId, bid, dbContext);
        return (newBid, error);
    }

    private static async Task UpdateCurrentLot(Guid lotId, decimal newBid, AuctionDbContext dbContext)
    {
        var lot = await dbContext.Lots.FindAsync(lotId);

        if (lot != null)
        {
            lot.CurrentPrice = newBid;
            await dbContext.SaveChangesAsync();
        }

    }
}