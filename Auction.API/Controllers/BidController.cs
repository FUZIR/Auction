using Auction.API.DTO;
using Auction.App.Interfaces;
using Auction.App.Models;
using Auction.App.Services;
using Auction.DataAccess.Postgres;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers;

[ApiController]
[Route("api")]
public class BidController(IBidService bidService):ControllerBase
{
    [HttpPost]
    [Route("bids/{id:guid}/create")]
    public async Task<ActionResult<Guid>> Create(Guid id, [FromBody] CreateBidDTO bidDto, [FromServices] AuctionDbContext dbContext)
    {
        var (bidModel, error) = await BidModel.Create(
            Guid.NewGuid(),
            id,
            bidDto.userId,
            bidDto.bid,
            DateTime.UtcNow,
            dbContext);
        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        await bidService.CreateBid(bidModel.Id, bidModel.LotId, bidModel.UserId, bidModel.Bid, bidModel.TimeStamp);
        return Ok(bidModel.Id);
    }
    
}