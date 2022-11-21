using DataAccess;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/Items")]
    public class ItemsController : ControllerBase
    {
        public class ItemID { public Guid ItemId { get; set; } }

        private readonly IDataAccessor _accessor;
        private readonly IList<Item> _items;

        public ItemsController(IDataAccessor dataAccessor)
        {
            _accessor = dataAccessor;
            _items = _accessor.GetItems();
        }

        // GET: api/Items
        public ActionResult GetItems() 
        {
            return Ok(_items);
        }

        // Put: api/Item
        [HttpPost]
        public ActionResult Create([FromBody] Item item) 
        {
            if (item.ItemId != Guid.Empty) return BadRequest("Wrong item id");
            if (string.IsNullOrEmpty(item.Name)) return BadRequest("Null or empty item name");
            if (item.Number < 0) return BadRequest("Negative item number");
            item.ItemId = Guid.NewGuid();
            _items.Add(item);
            _accessor.SaveItems();
            return Created("", item);
        }

        // POST: api/Item
        [HttpPut]
        public ActionResult Edit([FromBody] Item item) 
        {
            var changedItem = _items.First(it => it.Equals(item));
            if (changedItem is null) return BadRequest("No stored item with this id");
            changedItem.Name = item.Name;
            changedItem.Number = item.Number;
            _accessor.SaveItems();
            return Accepted();
        }
        // DELETE: api/Item
        [HttpDelete]
        public ActionResult Delete([FromBody] ItemID id) 
        {
            _items.Remove(_items.First(item => item.ItemId == id.ItemId));
            _accessor.SaveItems();
            return Accepted();
        }
    }
}
