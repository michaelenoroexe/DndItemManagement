using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Item;

namespace API.Controllers
{
    [ApiController]
    [Route("/api")]
    public class ItemController : ControllerBase
    {
        private readonly IServiceManager service;

        public ItemController(IServiceManager service) => this.service = service;

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

            return Created($"dm/{dmId}/rooms/{roomId}/items/{item.Id}", item);
        }

        [HttpDelete("dm/{dmId}/rooms/{roomId}/items/{id}")]
        public async Task<IActionResult> DeleteItemForRoom(int dmId, int roomId, int id)
        {
            await service.ItemService
                .DeleteItemAsync(roomId, id, true);

            return NoContent();
        }

        [HttpPut]
        [Route("dm/{dmId}/rooms/{roomId}/items/{id}")]
        public async Task<IActionResult> UpdateItemForDm
            (int dmId, int roomId, int id, [FromBody] ItemForUpdateDto itemForUpdate)
        {
            await service.ItemService.UpdateItemAsync(roomId, id, itemForUpdate, false, true);

            return NoContent();
        }

        [HttpPut]
        [Route("rooms/{roomId}/items/{id}")]
        public async Task<IActionResult> UpdateItemForRoom
            (int dmId, int roomId, int id, [FromBody] ItemForUpdateDto itemForUpdate)
        {
            var room = await service.RoomService.GetFullRoomAsync(id, false);

            await service.ItemService.UpdateItemAsync(room.Id, id, itemForUpdate, false, true);

            return NoContent();
        }

        [HttpPatch("dm/{dmId}/rooms/{roomId}/items/{id}")]
        public async Task<IActionResult> PatchItemForRoom
            (int dmId, int roomId, int id, [FromBody] JsonPatchDocument<ItemForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("pathDoc object sent from client is null");

            var result = await service.ItemService.GetItemForPatchAsync(roomId, id, false, true);

            patchDoc.ApplyTo(result.itemToPatch, ModelState);

            TryValidateModel(result.itemToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await service.ItemService.SaveChangesForPatchAsync(result.itemToPatch, result.itemEntity);

            return NoContent();
        }
    }
}
