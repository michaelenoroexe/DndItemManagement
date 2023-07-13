using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Character;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("/api")]
    public class CharacterController : ControllerBase
    {
        private readonly IServiceManager service;

        public CharacterController(IServiceManager service) => this.service = service;

        [HttpGet]
        [Route("rooms/{roomId}/characters")]
        public async Task<IActionResult> GetRoomCharacters(int roomId)
        {
            var characters = await service.CharacterService.GetRoomCharacters(roomId, false);

            return Ok(characters);
        }

        [HttpPost]
        [Route("rooms/{roomId}/characters")]
        public async Task<IActionResult> CreateCharacterForRoom
            (int roomId, [FromBody] CharacterForCreationDto characterForCreation)
        {
            var character = await service.CharacterService
                .CreateRoomCharacterAsync(roomId, characterForCreation, true);

            return Created($"rooms/{roomId}/characters/{character.Id}", character);
        }

        [HttpDelete]
        [Route("rooms/{roomId}/characters/{id}")]
        public async Task<IActionResult> DeleteCharacterForRoom (int roomId, int id)
        {
            await service.CharacterService.DeleteCharacterAsync(roomId, id, true);

            return NoContent();
        }

        [HttpPut]
        [Route("rooms/{roomId}/characters/{id}")]
        public async Task<IActionResult> UpdateCharacterForRoom
            (int roomId, int id, [FromBody] CharacterForUpdateDto characterForUpdate)
        {
            await service.CharacterService.UpdateCharacterAsync(roomId, id, characterForUpdate, false, true);

            return NoContent();
        }
    }
}
