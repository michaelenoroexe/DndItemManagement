using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.CharacterItem;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/rooms/{roomId}/character/{characterId}/chItems")]
    [Route("/api/dm/{dmId}/rooms/{roomId}/character/{characterId}/chItems")]
    public class CharacterItemController : ControllerBase
    {
        private readonly IServiceManager service;

        public CharacterItemController(IServiceManager service) => this.service = service;

        [HttpGet]
        public async Task<IActionResult> GetCharacterItems(int characterId)
        {
            var chItems = await service.CharacterItemsService.GetCharacterItemsAsync(characterId, false);

            return Ok(chItems);
        }

        [HttpPost("{itemId}")]
        public async Task<IActionResult> GiveItemToCharacter
            (int roomId, int characterId, int itemId, [FromBody] CharacterItemForCreationDto itemParameters)
        {
            var chItem = await service.CharacterItemsService
                .CreateCharacterItemAsync(characterId, itemId, itemParameters, true);

            return Created($"/api/rooms/{roomId}/character/{characterId}/chItems{itemId}", chItem);
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteCharactersItem
            (int characterId, int itemId)
        {
            await service.CharacterItemsService
                .DeleteCharacterItemAsync(characterId, itemId, true);

            return NoContent();
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateCharacterItemParameters
            (int characterId, int itemId, [FromBody] CharacterItemForUpdateDto itemParameters)
        {
            await service.CharacterItemsService.UpdateCharacterItemAsync
                (characterId, itemId, itemParameters, true);

            return NoContent();
        }

        [HttpPatch("{itemId}")]
        public async Task<IActionResult> UpdateCharacterItemParameters
            (int roomId, int characterId, int itemId, 
            [FromBody] JsonPatchDocument<CharacterItemForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result = await service.CharacterItemsService.GetCharacterItemForPatchAsync
                (characterId, itemId, true);

            patchDoc.ApplyTo(result.chItemToPatch);

            TryValidateModel(result.chItemToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await service.CharacterItemsService.SaveChangesForPatchAsync
                (result.chItemToPatch, result.chItemEntity);

            return NoContent();
        }

        [HttpOptions]
        public IActionResult RoomOptions()
        {
            Response.Headers.Add("Allow",
                "GET, " +
                "POST, " +
                "PUT, " +
                "DELETE, " +
                "PATCH, " +
                "OPTIONS"
                );

            return Ok();
        }
    }
}
