using Auction.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.DataAccess.Postgres.Repositories;

public class UserRepository
{
    private readonly AuctionDbContext _dbContext;

    public UserRepository(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Register(string email, string password, string nickname)
    {
        var user = new UserEntity()
        {
            Id = new Guid(),
            Email = email,
            Password = password,
            Nickname = nickname,

        };
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user.Id;
    }
    
    public async Task<Guid> Login(string email, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        return user.Id;
    }
    
}