namespace Auction.API.DTO;

public record CreateLotDTO(
    Guid id,
    string name,
    string description,
    decimal startprice,
    decimal buyprice,
    Guid creatorId,
    Guid buyerId);