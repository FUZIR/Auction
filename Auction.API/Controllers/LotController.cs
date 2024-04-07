using Auction.API.DTO;
using Auction.App.Interfaces;
using Auction.App.Models;
using Auction.DataAccess.Postgres;
using Auction.DataAccess.Postgres.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers;


[ApiController]
[Route("api/")]
public class LotController(ILotService lotService):ControllerBase
{
    [HttpPost]
    [Route("lots/create")]
    public async Task<ActionResult<Guid>> Create([FromBody]CreateLotDTO lotDTO, [FromServices] AuctionDbContext dbContext)
    {
        var (lotModel, error) = await LotModel.Create(Guid.NewGuid(),
            lotDTO.name,
            lotDTO.description,
            lotDTO.startprice,
            null,
            null,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            Status.ACTIVE,
            lotDTO.creatorId,
            null,
            dbContext);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        await lotService.Create(lotModel.Id, lotModel.Name, lotModel.Description, lotModel.StartingPrice,
            lotModel.BuyPrice, lotModel.CreatorId, lotModel.BuyerId);
        return Ok(lotModel.Id);
    }
}