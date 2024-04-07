using Auction.App.Interfaces;
using Auction.App.Models;
using Auction.App.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Auction.DataAccess.Postgres;
using Auction.DataAccess.Postgres.Interfaces;
using Auction.DataAccess.Postgres.Repositories;

public class Startup
{
    private IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AuctionDbContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Transient);
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILotRepository, LotRepository>();
        services.AddScoped<ILotService, LotService>();
        services.AddScoped<IBidRepository, BidRepository>();
        services.AddScoped<IBidService, BidService>();
        
        // Other service configurations...
    }

    // Other methods...
}