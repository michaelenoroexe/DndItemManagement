using API.Hubs;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<ItemHub> itemHub;

        public CharacterItemController(IServiceManager service, IHubContext<ItemHub> itemHub)
        {
            this.service = service;
            this.itemHub = itemHub;
        }

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

            await itemHub.Clients.Group("c" + characterId).SendAsync("AddedCharacterItem", chItem);

            return Created($"/api/rooms/{roomId}/character/{characterId}/chItems{itemId}", chItem);
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteCharactersItem
            (int characterId, int itemId)
        {
            await service.CharacterItemsService
                .DeleteCharacterItemAsync(characterId, itemId, true);

            await itemHub.Clients.Group("c" + characterId).SendAsync("DeletedCharacterItem", new { CharacterId = characterId, ItemId = itemId });

            return NoContent();
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateCharacterItemParameters
            (int characterId, int itemId, [FromBody] CharacterItemForUpdateDto itemParameters)
        {
            var chItem = await service.CharacterItemsService.UpdateCharacterItemAsync
                (characterId, itemId, itemParameters, true);

            await itemHub.Clients.Group("c" + characterId).SendAsync("UpdatedCharacterItem", chItem);

            return NoContent();
        }

        [HttpPatch("{itemId}")]
        public async Task<IActionResult> UpdatePartCharacterItemParameters
            (int characterId, int itemId, 
            [FromBody] CharacterItemForUpdateDto itemParameters)
        {
            var chItem = await service.CharacterItemsService
                .UpdateCharacterItemAsync(characterId, itemId, itemParameters, true);

            await itemHub.Clients.Group("c" + characterId).SendAsync("UpdatedCharacterItem", chItem);

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
