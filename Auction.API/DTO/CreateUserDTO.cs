namespace Auction.API.DTO;

public record CreateUserDTO(
    string email,
    string password,
    string nickname);