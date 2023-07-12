using AutoMapper;
using Administration.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Administration.Service.Contracts;
using Administration.Shared.DataTransferObjects.Room;
using System.Security.Claims;
using Administration.Hubs;

namespace Administration.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;
        private readonly IAuthenticationService authService;
        private readonly IHubContext<RoomHub> roomHub;

        public RoomController(IRoomService roomService, IAuthenticationService authService, IHubContext<RoomHub> hub)
        {
            this.roomService = roomService;
            this.authService = authService;
            this.roomHub = hub;
        }
        [HttpGet("rooms/{id}")]
        public async Task<IActionResult> GetRooms(int id)
        {
            var room = await roomService.GetRoomAsync(id, false);

            return Ok(room);
        }
        [HttpGet("dm/{dmId}/rooms")]
        public async Task<IActionResult> GetDmRooms(int dmId)
        {
            var rooms = await roomService.GetRoomsForDM(dmId, false);

            return Ok(rooms);
        }
        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms()
        {
            var dms = await roomService.GetAllRooms(false);

            return Ok(dms);
        }
        [HttpPost("rooms/auth")]
        public async Task<ActionResult> SignInRoom(RoomForAuthenticationDto room)
        {
            var valResult = await authService.ValidateRoom(room);
            if (valResult == false) throw new UnauthorizedAccessException();
            int? dmId = HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value is string dI? Convert.ToInt32(dI) : null ;
            string token = authService.CreateToken(dmId, room.CharacterId);
            return Ok(new { Tocken = token });
        }
        [Authorize]
        [HttpPost("dm/{dmId}/rooms")]
        public async Task<IActionResult> CreateRoom(int dmId, [FromBody] RoomForCreationDto roomForRegistration)
        {
            var dm = User.FindFirst(ClaimTypes.Name)!.Value;
            var room = await roomService.CreateRoomForDMAsync(dmId, roomForRegistration, true);
            await roomHub.Clients.All.SendAsync("AddedRoom",
                new RoomWithDMDto[] { new RoomWithDMDto(room.Id, room.Name, room.Started, dm) }
            );
            return Created($"/api/dm/rooms/{room.Id}", room);
        }
        [Authorize]
        [HttpDelete("dm/{dmId}/rooms/{id:int}")]
        public async Task<IActionResult> DeleteRoom(int dmId, int id)
        {
            await roomService.DeleteRoomAsync(dmId, id, true);
            await roomHub.Clients.All.SendAsync("DeletedRoom", new int[] { id });
            return NoContent();
        }
        [Authorize]
        [HttpPut("dm/{dmId}/rooms/{id:int}")]
        public async Task<IActionResult> UpdateRoom(int dmId, int id, [FromBody] RoomForUpdateDto roomForUpdate)
        {
            await roomService.UpdateRoomAsync(dmId, id, roomForUpdate, false, true);
            await roomHub.Clients.All.SendAsync("ChangedRoom", roomForUpdate);
            return NoContent();
        }
        [Authorize]
        [HttpPatch("dm/{dmId}/rooms/{id:int}")]
        public async Task<IActionResult> PartiallyUpdateRoom(int dmId, int id,
            [FromBody] RoomForUpdateDto roomForUpdate)
        {
            await roomService.PartialUpdateRoomAsync(dmId, id, roomForUpdate, false, true);
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
