namespace Auction.App.Interfaces;

public interface IUserRepository
{
    public Task<Guid?> Register(Guid id, string email, string password, string nickname);

    public Task<Guid?> Login(string email, string password);
}