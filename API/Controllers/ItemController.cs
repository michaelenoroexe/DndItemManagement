using API.Hubs;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.Contracts;
using Shared.DataTransferObjects.Item;

namespace API.Controllers
{
    [ApiController]
    [Route("/api")]
    public class ItemController : ControllerBase
    {
        private readonly IServiceManager service;
        private readonly IHubContext<ItemHub> itemHub;

        public ItemController(IServiceManager service, IHubContext<ItemHub> itemHub)
        {
            this.service = service;
            this.itemHub = itemHub;
        }

        [HttpGet("category/{categoryId}/items")]
        public async Task<IActionResult> GetCategoryItems(int categoryId)
        {
            var items = await service.ItemService
                .GetAllItemsForCategoryAsync(categoryId, false);

            return Ok(items);
        }
        [HttpGet("items")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await service.ItemService.GetAllItemsAsync(false);

            return Ok(items);
        }
        [HttpGet("dm/{dmId}/items")]
        public async Task<IActionResult> GetAllItemsForDM(int dmId)
        {
            var items = await service.ItemService.GetAllItemsForDmAsync(dmId, false);

            return Ok(items);
        }

        [HttpPost("dm/{dmId}/rooms/{roomId}/items")]
        public async Task<IActionResult> PostItemForRoom(int dmId, int roomId,
            [FromBody] ItemForCreationDto itemForCreation)
        {
            var item = await service.ItemService
                .CreateItemAsync(roomId, itemForCreation, true);

            await itemHub.Clients.Group("r" + roomId).SendAsync("AddedItem", item);

            return Created($"dm/{dmId}/rooms/{roomId}/items/{item.Id}", item);
        }

        [HttpDelete("dm/{dmId}/rooms/{roomId}/items/{id}")]
        public async Task<IActionResult> DeleteItemForRoom(int dmId, int roomId, int id)
        {
            await service.ItemService
                .DeleteItemAsync(roomId, id, true);

            await itemHub.Clients.Group("r" + roomId).SendAsync("DeletedItem", id);

            return NoContent();
        }

        [HttpPut]
        [Route("rooms/{roomId}/items/{id}")]
        [Route("dm/{dmId}/rooms/{roomId}/items/{id}")]
        public async Task<IActionResult> UpdateItemForRoom
            (int dmId, int roomId, int id, [FromBody] ItemForUpdateDto itemForUpdate)
        {
            var item = await service.ItemService.UpdateItemAsync(roomId, id, itemForUpdate, false, true);

            await itemHub.Clients.Group("r" + roomId).SendAsync("UpdatedItem", item);

            return NoContent();
        }

        [HttpPatch]
        [Route("rooms/{roomId}/items/{id}")]
        [Route("dm/{dmId}/rooms/{roomId}/items/{id}")]
        public async Task<IActionResult> PatchItemForRoom
            (int dmId, int roomId, int id, [FromBody] ItemForPatchDto itemDto)
        {
            var item = await service.ItemService.PartialUpdateItemAsync(roomId,  id, itemDto, false, true);

            await itemHub.Clients.Group("r" + roomId).SendAsync("UpdatedItem", item);

            return NoContent();
        }
    }
}
