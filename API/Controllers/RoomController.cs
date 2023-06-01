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

        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms()
        {
            var dms = await service.RoomService.GetAllRooms(false);

            return Ok(dms);
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
            await roomHub.Clients.All.SendAsync("DeleteRoom", new int[] { id });
            return NoContent();
        }
        [Authorize]
        [HttpPut("dm/{dmId}/rooms/{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateRoom(int dmId, int id, [FromBody] RoomForUpdateDto roomForUpdate)
        {
            await service.RoomService.UpdateRoomAsync(dmId, id, roomForUpdate, false, true);
            await roomHub.Clients.All.SendAsync("ChangeRoom", roomForUpdate);
            return NoContent();
        }
        [Authorize]
        [HttpPut("dm/{dmId}/rooms/{id:int}/pass")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateFullRoom(int dmId, int id, [FromBody] RoomWithPassDto roomForUpdate)
        {
            await service.RoomService.UpdateRoomAsync(dmId, id, roomForUpdate, false, true);

            return NoContent();
        }
        [HttpPatch("dm/{dmId}/rooms/{id:int}")]
        public async Task<IActionResult> PartiallyUpdateRoom(int dmId, int id,
            [FromBody] JsonPatchDocument<RoomForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result = await service.RoomService.GetRoomForPatchAsync(dmId, id, false, true);

            patchDoc.ApplyTo(result.roomToPatch, ModelState);

            TryValidateModel(result.roomToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await service.RoomService.SaveChangesForPatchAsync(result.roomToPatch, result.roomEntity);

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
