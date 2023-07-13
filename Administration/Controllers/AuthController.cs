using System.Security.Claims;
using System.Text.Json;
using Administration.Service;
using Administration.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
  private readonly ILogger<AuthController> logger;
  private readonly IRoomService roomService;

  public AuthController(IRoomService roomService, ILogger<AuthController> logger)
  {
    this.roomService = roomService;
    this.logger = logger;
  }

  [Route("/auth")]
  public async Task<IActionResult> AuthHandling()
  {
    var originalUri = Request.Headers["X-Original-URI"].First();
    if (originalUri is null) NoContent();
    var pathParams = originalUri!.Split("/");

    var originalMethod = Request.Headers["X-Original-METHOD"].First();
    if (pathParams[pathParams.Length-1] == "characters" && 
        originalMethod == "GET") return Ok();

    if (!User.Claims.Any()) return Unauthorized();
    var userRoomId = User.FindFirst(ClaimTypes.Actor)?.Value;
    var requestRoomId = pathParams[pathParams.ToList().IndexOf("rooms") + 1];
    if (requestRoomId == userRoomId) return Ok();

    if (int.TryParse(User.FindFirst(ClaimTypes.Name)?.Value, out int userDmId)) 
    {
        var dmRooms = await roomService.GetRoomsForDM(userDmId, false);

        if (dmRooms.Any(r => r.Id == int.Parse(requestRoomId))) return Ok();
    }

    return Forbid();
  }
}