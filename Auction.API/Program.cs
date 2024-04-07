using Auction.App.Interfaces;
using Auction.App.Services;
using Auction.DataAccess.Postgres;
using Auction.DataAccess.Postgres.Interfaces;
using Auction.DataAccess.Postgres.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILotRepository, LotRepository>();
builder.Services.AddScoped<ILotService, LotService>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IBidService, BidService>();


builder.Services.AddDbContext<AuctionDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(AuctionDbContext)));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
