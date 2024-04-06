using Auction.App.Interfaces;

namespace Auction.App.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<Guid?> Register(Guid id, string email, string password, string nickname)
    {
        return await userRepository.Register(id, email, password, nickname);
    }

    public async Task<Guid?> Login(string email, string password)
    {
        return await userRepository.Login(email, password);
    }
}