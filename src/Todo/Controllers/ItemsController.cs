using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Todo.Data;
using Todo.Model;

namespace Todo.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemsRepository;

        public ItemsController(IItemRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        [HttpPost]
        public async Task<Item> CreateItem(ItemPost body)
        {
            var item = await _itemsRepository.Insert(
                body.user_id,
                body.board_id,
                body.title,
                body.description,
                body.status,
                body.due_date);

            return Item.FromData(item);
        }

        [HttpPut("{item_id}")]
        public async Task UpdateItem(string item_id, ItemPut body)
        {
            await _itemsRepository.Update(
                item_id,
                body.board_id,
                body.title,
                body.description,
                body.status,
                body.due_date);
        }

        [HttpGet("{item_id}")]
        public async Task<ActionResult<Item>> GetItemById(string item_id)
        {
            var item = await _itemsRepository.GetById(item_id);
            if (item == null) {
                return NotFound();
            }

            return Item.FromData(item);
        }

        [HttpGet]
        public async Task<ItemCollection> GetItems([FromQuery] string user_id, [FromQuery] string board_id)
        {
            var items = await _itemsRepository.GetByBoardId(user_id, board_id);
            
            return new ItemCollection {
                items = items.Select(Item.FromData).ToArray()
            };
        }

        [HttpDelete("{item_id}")]
        public async Task DeleteItem(string item_id)
        {
            await _itemsRepository.Delete(item_id);
        }
    }

    public record ItemPost : ItemPut
    {
        public string user_id { get; init; }
    }

    public record ItemPut
    {
        public string board_id { get; init; }
        public string title { get; init; }
        public string description { get; init; }
        public string status { get; init; }
        public DateTime? due_date { get; init; }
    }
}
