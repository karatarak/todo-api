using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Controllers.Model;
using System.ComponentModel.DataAnnotations;
using Todo.Controllers.Validation;

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
            var item = await _itemsRepository.CreateItem(
                body.user_id,
                body.board_id,
                body.title,
                body.description,
                body.status,
                body.due_date);

            return Item.FromData(item);
        }

        [HttpPut("{item_id:guid}")]
        public async Task UpdateItem(Guid item_id, ItemPut body)
        {
            await _itemsRepository.UpdateItem(
                item_id,
                body.title,
                body.description,
                body.status,
                body.due_date);
        }

        [HttpDelete("{item_id:guid}")]
        public async Task DeleteItem(Guid item_id)
        {
            await _itemsRepository.DeleteItem(item_id);
        }

        [HttpGet("{item_id:guid}")]
        public async Task<ActionResult<Item>> GetItemById(Guid item_id)
        {
            var item = await _itemsRepository.GetItemById(item_id);
            if (item == null) {
                return NotFound();
            }

            return Item.FromData(item);
        }

        [HttpGet]
        public async Task<ItemCollection> GetItems([FromQuery] Guid user_id)
        {
            var items = await _itemsRepository.GetItemsByUserId(user_id);
            
            return new ItemCollection {
                items = items.Select(Item.FromData).ToArray()
            };
        }
    }

    public class ItemPost
    {
        [NotEmpty]
        public Guid user_id { get; init; }
        [NotEmpty]
        public Guid board_id { get; init; }
        [Required]
        public string title { get; init; }
        public string description { get; init; }
        public string status { get; init; }
        public DateTime? due_date { get; init; }
    }

    public class ItemPut
    {
        public string title { get; init; }
        public string description { get; init; }
        public string status { get; init; }
        public DateTime? due_date { get; init; }
    }
}
