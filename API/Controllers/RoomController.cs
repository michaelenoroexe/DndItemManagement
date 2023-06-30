using API.ActionFilters;
using API.Hubs;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.Contracts;
using Shared.DataTransferObjects.Room;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class RoomController : ControllerBase
    {
        private readonly IServiceManager service;
        private readonly IHubContext<RoomHub> roomHub;

        public RoomController(IServiceManager service, IHubContext<RoomHub> hub)
        {
            this.service = service;
            this.roomHub = hub;
        }
        [HttpGet("rooms/{id}")]
        public async Task<IActionResult> GetRooms(int id)
        {
            var room = await service.RoomService.GetRoomAsync(id, false);

            return Ok(room);
        }
        [HttpGet("dm/{dmId}/rooms")]
        public async Task<IActionResult> GetDmRooms(int dmId)
        {
            var rooms = await service.RoomService.GetRoomsForDM(dmId, false);

            return Ok(rooms);
        }
        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms()
        {
            var dms = await service.RoomService.GetAllRooms(false);

            return Ok(dms);
        }
        [HttpPost("auth")]
        public async Task<ActionResult> SignInRoom(RoomForAuthenticationDto room)
        {
            var valResult = await service.AuthenticationService.ValidateRoom(room);
            if (valResult == false) throw new UnauthorizedAccessException();
            var dmLogin = HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;
            string token = service.AuthenticationService.CreateToken(dmLogin, room.CharacterId);
            return Ok(new { Tocken = token });
        }
        [Authorize]
        [HttpPost("dm/{dmId}/rooms")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateRoom(int dmId, [FromBody] RoomForCreationDto roomForRegistration)
        {
            var dm = User.FindFirst(ClaimTypes.Name)!.Value;
            var room = await service.RoomService.CreateRoomForDMAsync(dmId, roomForRegistration, true);
            await roomHub.Clients.All.SendAsync("AddedRoom",
                new RoomWithDMDto[] { new RoomWithDMDto(room.Id, room.Name, room.Started, dm) }
            );
            return Created($"/api/dm/rooms/{room.Id}", room);
        }
        [Authorize]
        [HttpDelete("dm/{dmId}/rooms/{id:int}")]
        public async Task<IActionResult> DeleteRoom(int dmId, int id)
        {
            await service.RoomService.DeleteRoomAsync(dmId, id, true);
            await roomHub.Clients.All.SendAsync("DeletedRoom", new int[] { id });
            return NoContent();
        }
        [Authorize]
        [HttpPut("dm/{dmId}/rooms/{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateRoom(int dmId, int id, [FromBody] RoomForUpdateDto roomForUpdate)
        {
            await service.RoomService.UpdateRoomAsync(dmId, id, roomForUpdate, false, true);
            await roomHub.Clients.All.SendAsync("ChangedRoom", roomForUpdate);
            return NoContent();
        }
        [Authorize]
        [HttpPatch("dm/{dmId}/rooms/{id:int}")]
        public async Task<IActionResult> PartiallyUpdateRoom(int dmId, int id,
            [FromBody] RoomForUpdateDto roomForUpdate)
        {
            await service.RoomService.PartialUpdateRoomAsync(dmId, id, roomForUpdate, false, true);
            await roomHub.Clients.All.SendAsync("ChangedRoom", roomForUpdate);

            return NoContent();
        }
        [HttpOptions("rooms")]
        public IActionResult RoomOptionsFull()
        {
            Response.Headers.Add("Allow",
                "GET"
                );

            return Ok();
        }
        [HttpOptions("dm/{dmId}/rooms/")]
        public IActionResult RoomOptions()
        {
            Response.Headers.Add("Allow",
                "GET, " +
                "POST, " +
                "PUT, " +
                "DELETE, " +
                "OPTIONS"
                );

            return Ok();
        }
    }
}
