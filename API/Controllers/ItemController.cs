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

        [HttpGet("dm/{dmId}/items")]
        public async Task<IActionResult> GetAllItemsForDm(int dmId)
        {
            var items = await service.ItemService.GetAllItemsForDmAsync(dmId, false);

            return Ok(items);
        }

        [HttpPost("dm/{dmId}/items")]
        public async Task<IActionResult> PostItemForDm(int dmId,
            [FromBody] ItemForCreationDto itemForCreation)
        {
            var item = await service.ItemService
                .CreateItemAsync(dmId, itemForCreation, true);

            return Created($"dm/{dmId}/items/{item.Id}", item);
        }

        [HttpDelete("dm/{dmId}/items/{id}")]
        public async Task<IActionResult> DeleteItemForDm(int dmId, int id)
        {
            await service.ItemService
                .DeleteItemAsync(dmId, id, true);

            return NoContent();
        }

        [HttpPut]
        [Route("dm/{dmId}/items/{id}")]
        public async Task<IActionResult> UpdateItemForDm
            (int dmId, int id, [FromBody] ItemForUpdateDto itemForUpdate)
        {
            await service.ItemService.UpdateItemAsync(dmId, id, itemForUpdate, false, true);

            return NoContent();
        }

        [HttpPut]
        [Route("rooms/{roomId}/items/{id}")]
        public async Task<IActionResult> UpdateItemForRoom
            (int roomId, int id, [FromBody] ItemForUpdateDto itemForUpdate)
        {
            var room = await service.RoomService.GetFullRoomAsync(id, false);

            await service.ItemService.UpdateItemAsync(room.DmId, id, itemForUpdate, false, true);

            return NoContent();
        }

        [HttpPatch("dm/{dmId}/items/{id}")]
        public async Task<IActionResult> PatchItemForDm
            (int dmId, int id, [FromBody] JsonPatchDocument<ItemForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("pathDoc object sent from client is null");

            var result = await service.ItemService.GetItemForPatchAsync(dmId, id, false, true);

            patchDoc.ApplyTo(result.itemToPatch, ModelState);

            TryValidateModel(result.itemToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await service.ItemService.SaveChangesForPatchAsync(result.itemToPatch, result.itemEntity);

            return NoContent();
        }
    }
}
