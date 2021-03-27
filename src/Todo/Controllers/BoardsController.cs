using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Model;

namespace Todo.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly IItemsRepository _itemsRepository;

        public BoardsController(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        [HttpGet("{board_id}")]
        public async Task<Board> GetBoardById(string board_id, [FromQuery] string user_id)
        {
            var items = await _itemsRepository.GetByUserId(user_id, board_id);
            return Board.FromData(user_id, board_id, items);
        }
    }
}
