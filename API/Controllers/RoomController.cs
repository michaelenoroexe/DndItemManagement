using API.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Room;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class RoomController : ControllerBase
    {
        private readonly IServiceManager service;

        public RoomController(IServiceManager service) => this.service = service;

        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms()
        {
            var dms = await service.RoomService.GetAllRooms(false);

            return Ok(dms);
        }
        [HttpPost("dm/{dmId}/rooms")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateRoom(int dmId, [FromBody] RoomForCreationDto roomForRegistration)
        {
            var room = await service.RoomService.CreateRoomForDMAsync(dmId, roomForRegistration, true);

            return Created($"/api/dm/rooms/{room.Id}", room);
        }
        [HttpDelete("dm/{dmId}/rooms/{id:int}")]
        public async Task<IActionResult> DeleteRoom(int dmId, int id)
        {
            await service.RoomService.DeleteRoomAsync(dmId, id, true);

            return NoContent();
        }
        [HttpPut("dm/{dmId}/rooms/{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateRoom(int dmId, int id, [FromBody] RoomForUpdateDto roomForUpdate)
        {
            await service.RoomService.UpdateRoomAsync (dmId, id, roomForUpdate, false, true);

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
