namespace Auction.API.DTO;

public record CreateLotDTO(
    string name,
    string description,
    decimal startprice,
    Guid creatorId);