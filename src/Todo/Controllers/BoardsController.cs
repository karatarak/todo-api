using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Controllers.Model;

namespace Todo.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardRepository _boardRepository;

        public BoardsController(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        [HttpPost]
        public async Task<Board> CreateBoard(BoardPost body)
        {
            var board = await _boardRepository.CreateBoard(body.user_id, body.title);
            return Board.FromData(board);
        }

        [HttpPut("{board_id:guid}")]
        public async Task UpdateBoard(Guid board_id, BoardPut body)
        {
            await _boardRepository.UpdateBoard(board_id, body.title);
        }

        [HttpDelete("{board_id:guid}")]
        public async Task DeleteBoard(Guid board_id)
        {
            await _boardRepository.DeleteBoard(board_id);
        }

        [HttpGet("{board_id}")]
        public async Task<Board> GetBoardById(Guid board_id)
        {
            var boardItems = await _boardRepository.GetBoardById(board_id);
            return Board.FromData(boardItems);
        }

        [HttpGet]
        public async Task<BoardCollection> GetBoards([FromQuery] Guid user_id)
        {
            var boards = await _boardRepository.GetBoardsByUserId(user_id);
            
            return new BoardCollection {
                boards = boards.Select(Board.FromData).ToArray()
            };
        }
    }

    public class BoardPost : BoardPut
    {
        public Guid user_id { get; init; }
    }

    public class BoardPut
    {
        public string title { get; init; }
    }
}
