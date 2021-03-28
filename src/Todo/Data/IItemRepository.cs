using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Data
{
    public interface IItemRepository
    {
        Task<ItemData> GetItemById(Guid itemId);

        Task<IList<ItemData>> GetItemsByUserId(Guid userId);

        Task<ItemData> CreateItem(Guid userId, Guid boardId, string title, string description, string status, DateTime? dueDate);
        
        Task<bool> UpdateItem(Guid itemId, string title, string description, string status, DateTime? dueDate);
        
        Task<bool> DeleteItem(Guid itemId);
    }
}
