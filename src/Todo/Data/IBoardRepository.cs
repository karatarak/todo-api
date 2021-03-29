using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Data
{
    public interface IBoardRepository
    {
        Task<BoardData> GetBoardById(Guid boardId);

        Task<IList<BoardData>> GetBoardsByUserId(Guid userId);

        Task<BoardData> CreateBoard(Guid userId, string title);
        
        Task<bool> UpdateBoard(Guid boardId, string title);
        
        Task<bool> DeleteBoard(Guid boardId);
    }
}
