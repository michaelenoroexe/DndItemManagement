using API.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.ItemCategory;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/itemcategories")]
    public class ItemCategoryController : ControllerBase
    {
        private readonly IServiceManager service;

        public ItemCategoryController(IServiceManager service) => this.service = service;

        [HttpGet]
        public async Task<IActionResult> GetAllItemCategories() 
        {
            var itemCategories = await service.ItemCategoryService.GetItemCategories(false);

            return Ok(itemCategories);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateItemCategory([FromBody] ItemCategoryForCreationDto itemCategForCreation)
        {
            var itemCategory = await service.ItemCategoryService.CreateItemCategoryAsync(itemCategForCreation, true);

            return Created("/api/itemcategories/", itemCategory);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteItemCategory(int id)
        {
            await service.ItemCategoryService.DeleteItemCategoryAsync(id, true);

            return NoContent();
        }
        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateItemCategory(int id, [FromBody] ItemCategoryForUpdateDto itemCategForUpdate)
        {
            await service.ItemCategoryService.UpdateItemCategoryAsync(id, itemCategForUpdate, true);

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetItemCategoryOptions()
        {
            Response.Headers.Add("Allow", 
                "GET, " +
                "OPTIONS"
                );

            return Ok();
        }
    }
}
