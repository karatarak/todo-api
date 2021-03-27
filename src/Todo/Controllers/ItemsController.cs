using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Todo.Data;

namespace Todo.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemsController(IItemsRepository itemsRepository)
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

            return Map(item);
        }

        [HttpPut("{item_id:guid}")]
        public async Task UpdateItem(Guid item_id, ItemPut body)
        {
            await _itemsRepository.Update(
                item_id,
                body.board_id,
                body.title,
                body.description,
                body.status,
                body.due_date);
        }

        [HttpGet("{item_id:guid}")]
        public async Task<ActionResult<Item>> GetItemById(Guid item_id)
        {
            var item = await _itemsRepository.GetById(item_id);
            if (item == null) {
                return NotFound();
            }

            return Map(item);
        }

        [HttpGet]
        public async Task<ItemCollection> GetItems([FromQuery] string user_id, [FromQuery] string board_id)
        {
            var items = await _itemsRepository.GetByBoardId(user_id, board_id);
            
            return new ItemCollection {
                items = items.Select(Map).ToArray()
            };
        }

        [HttpDelete("itemId:guid")]
        public async Task DeleteItem(Guid itemId)
        {
            await _itemsRepository.Delete(itemId);
        }

        private static Item Map(ItemData data)
        {
            return new Item {
                item_id = data.item_id,
                user_id = data.user_id,
                board_id = data.board_id,
                title = data.title,
                description = data.description,
                status = data.status,
                created_date = data.created_date,
                due_date = data.due_date,
            };
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

    public record Item
    {
        public Guid item_id { get; init; }
        public string user_id { get; init; }
        public string board_id { get; init; }
        public string title { get; init; }
        public string description { get; init; }
        public string status { get; init; }
        public DateTime created_date { get; init; }
        public DateTime? due_date { get; init; }
    }

    public record ItemCollection
    {
        public Item[] items { get; init; }
    };
}
