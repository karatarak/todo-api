using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Data
{
    public class ItemsRepository : IItemsRepository
    {
        public static readonly string DefaultUserId = "peter";
        public static readonly string DefaultBoardId = "default";

        private readonly IList<ItemData> _items = new List<ItemData>();
        

        public ItemsRepository()
        {
            // Insert initial dataset
            Insert(DefaultUserId, DefaultBoardId, "Feed the tiger", "Mr Snuggles is getting hungry", "pending", null).Wait();
            Insert(DefaultUserId, DefaultBoardId, "Catch 20 Fish", "Level up my fishing skill", "pending", null).Wait();
            Insert(DefaultUserId, "calendar", "Training with Mr Miyagi", "Working towards my white belt", "pending", null).Wait();
        }

        public Task<ItemData> GetById(Guid itemId)
        {
            var item = _items.FirstOrDefault(x => x.item_id == itemId);
            return Task.FromResult(item);
        }


        public Task<IList<ItemData>> GetByUserId(string userId, string boardId)
        {
            var itemsQuery = _items.Where(x => x.user_id == userId);

            if (boardId is not null)
            {
                itemsQuery = itemsQuery.Where(x => x.board_id == boardId);
            }
                
            var items = (IList<ItemData>) itemsQuery
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
                board_id = boardId ?? DefaultBoardId,
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
                item.board_id = boardId ?? DefaultBoardId;
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
