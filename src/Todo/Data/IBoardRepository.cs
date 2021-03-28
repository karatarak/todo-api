using System;
using System.Threading.Tasks;

namespace Todo.Data
{
    public interface IBoardRepository
    {
        Task<BoardData> GetBoardById(Guid boardId);

        Task<BoardItemsData> GetBoardItemsById(Guid boardId);

        Task<BoardData> CreateBoard(Guid userId, string title);
        
        Task<bool> UpdateBoard(Guid boardId, string title);
        
        Task<bool> DeleteBoard(Guid boardId);
    }
}
