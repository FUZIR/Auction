using Auction.App.Interfaces;
using Auction.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.DataAccess.Postgres.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuctionDbContext _dbContext;

    public UserRepository(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid?> Register(Guid id, string email, string password, string nickname)
    {
        var newUser = new UserEntity()
        {
            Id = id,
            Email = email,
            Password = password,
            Nickname = nickname,

        };
        await _dbContext.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();
        return newUser.Id;
    }

    public async Task<Guid?> Login(string email, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        if (user == null)
        {
            return null;
        }
        return user.Id;
    }
    
}