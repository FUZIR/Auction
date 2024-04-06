using Auction.API.DTO;
using Auction.App.Interfaces;
using Auction.App.Models;
using Auction.DataAccess.Postgres;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers;

[ApiController]
[Route(("api/[controller]"))]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Register([FromBody]CreateUserDTO userDto, [FromServices]AuctionDbContext dbContext)
    {
        var (userModel, error) = UserModel.Create(
            Guid.NewGuid(),
            userDto.email,
            userDto.password,
            userDto.nickname,
            dbContext);
        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        await userService.Register(userModel.Id, userModel.Email, userModel.Password, userModel.Nickname);
        return Ok(new { Id = userModel.Id, Email = userModel.Email, Nickname = userModel.Nickname });
    }
}

