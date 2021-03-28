using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Data
{
    public interface IItemRepository
    {
        Task<ItemData> GetById(string itemId);

        Task<IEnumerable<ItemData>> GetByBoardId(string userId, string boardId);

        Task<ItemData> Insert(string userId, string boardId, string title, string description, string status, DateTime? dueDate);
        
        Task<bool> Update(string itemId, string boardId, string title, string description, string status, DateTime? dueDate);
        
        Task<bool> Delete(string itemId);
    }
}
