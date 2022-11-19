using DataAccess;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/Items")]
    public class ItemsController : ControllerBase
    {
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
            _items.Add(item);
            _accessor.SaveItems();
            return Accepted();
        }

        // POST: api/Item
        [HttpPut]
        public ActionResult Edit([FromBody] Item item) 
        {
            _items.First(item => item.Equals(item));
            return Accepted();
        }
        // DELETE: api/Item
        [HttpDelete]
        public ActionResult Delete([FromBody] int id) 
        {
            _items.Remove(_items.First(item => item.ItemId == id));
            return Accepted();
        }
    }
}
