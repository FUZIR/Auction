using Auction.App.Models;
using Auction.DataAccess.Postgres;
using Microsoft.Extensions.Hosting;

namespace Auction.App;

public class LotExpirationService : BackgroundService
{
    private readonly AuctionDbContext _dbContext;
    private readonly LotModel _lotModel;

    public LotExpirationService(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
        _lotModel = new LotModel(_dbContext);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _lotModel.StartTrackingLotExpirations(TimeSpan.FromMinutes(1));
    }
}