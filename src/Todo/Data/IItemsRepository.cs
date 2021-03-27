using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Data
{
    public interface IItemsRepository
    {
        Task<ItemData> GetById(Guid itemId);

        Task<IList<ItemData>> GetByUserId(string userId, string boardId);

        Task<ItemData> Insert(string userId, string boardId, string title, string description, string status, DateTime? dueDate);
        
        Task<bool> Update(Guid itemId, string boardId, string title, string description, string status, DateTime? dueDate);
        
        Task<bool> Delete(Guid itemId);
    }
}
