using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Data
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly IList<ItemData> _items = new List<ItemData>();

        public ItemsRepository()
        {
            // Insert initial dataset
            var userId = "peter";
            Insert(userId, null, "Feed the tiger", "Mr Snuggles is getting hungry", "pending", null).Wait();
            Insert(userId, null, "Catch 20 Fish", "Level up my fishing skill", "pending", null).Wait();
            Insert(userId, null, "Training with Mr Miyagi", "Working towards my white belt", "pending", null).Wait();
        }

        public Task<ItemData> GetById(Guid itemId)
        {
            var item = _items.FirstOrDefault(x => x.item_id == itemId);
            return Task.FromResult(item);
        }


        public Task<IList<ItemData>> GetByBoardId(string userId, string boardId)
        {
            var items = (IList<ItemData>) _items
                .Where(x => x.user_id == userId)
                .Where(x => x.board_id == boardId)
                .OrderBy(x => x.due_date)
                .ThenBy(x => x.created_date)
                .ToList();

            return Task.FromResult(items);
        }

        public Task<ItemData> Insert(string userId, string boardId, string title, string description, string status, DateTime? dueDate)
        {
            var item = new ItemData {
                item_id = Guid.NewGuid(),
                user_id = userId,
                board_id = boardId,
                title = title,
                description = description,
                status = status,
                created_date = DateTime.UtcNow,
                due_date = dueDate,
            };

            _items.Add(item);

            return Task.FromResult(item);
        }

        public Task<bool> Update(Guid itemId, string boardId, string title, string description, string status, DateTime? dueDate)
        {
            var item = _items.FirstOrDefault(x => x.item_id == itemId);
            
            if (item is not null)
            {
                item.board_id = boardId;
                item.title = title.Trim();
                item.description = description.Trim();
                item.status = status.ToLowerInvariant().Trim();
                item.due_date = dueDate;

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
        
        public Task<bool> Delete(Guid itemId)
        {
            var item = _items.FirstOrDefault(x => x.item_id == itemId);
            if (item != null)
            {
                _items.Remove(item);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
