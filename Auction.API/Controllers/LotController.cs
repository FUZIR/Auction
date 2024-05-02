using System.Text.Json;
using System.Text.Json.Serialization;
using Auction.API.DTO;
using Auction.App.Interfaces;
using Auction.App.Models;
using Auction.DataAccess.Postgres;
using Auction.DataAccess.Postgres.Entities;
using Auction.DataAccess.Postgres.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers;


[ApiController]
[Route("api/lots")]
public class LotController(ILotService lotService):ControllerBase
{
    JsonSerializerOptions options = new JsonSerializerOptions
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };
    
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Guid>> Create([FromBody]CreateLotDTO lotDTO, [FromServices] AuctionDbContext dbContext)
    {
        var (lotModel, error) = await LotModel.Create(Guid.NewGuid(),
            lotDTO.name,
            lotDTO.description,
            lotDTO.startprice,
            null,
            null,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(24),
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

    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult> CreateDelete([FromBody]Guid id, [FromServices]AuctionDbContext dbContext)
    {
        var (LotId, error) = await LotModel.Delete(id, dbContext);
        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        await lotService.Delete(LotId);
        return Ok();
    }

    [HttpGet]
    [Route("get")]
    public async Task<ActionResult<LotEntity>> Get([FromQuery] string id, [FromServices] AuctionDbContext dbContext)
    {
        Guid lotId;
        Guid.TryParse(id, out lotId);
        var (lot,error) = await LotModel.Get(lotId, dbContext);
        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        var json = JsonSerializer.Serialize(lot, options);
        return Ok(json);
    }

    [HttpGet]
    [Route("get_all")]
    public async Task<ActionResult<List<LotEntity>>> GetAll([FromServices] AuctionDbContext dbContext)
    {
        var (lots, error) = await LotModel.GetAll(dbContext);
        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        return Ok(JsonSerializer.Serialize(lots, options));
    }

    [HttpGet]
    [Route("get_user_lots")]
    public async Task<ActionResult<List<LotEntity>>> GetUserLots([FromQuery] string userId,[FromServices] AuctionDbContext dbContext)
    {
        Guid id;
        Guid.TryParse(userId, out id);
        var (lots, error) = await LotModel.GetAllUserLots(id, dbContext);
        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        
        return Ok(JsonSerializer.Serialize(lots, options));
    }
}