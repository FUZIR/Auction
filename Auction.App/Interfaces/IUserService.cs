namespace Auction.App.Interfaces;

public interface IUserService
{
    Task<Guid?> Register(Guid id, string email, string password, string nickname);
    Task<Guid?> Login(string email, string password);
}