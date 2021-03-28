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
        private readonly IItemRepository _itemsRepository;

        public BoardsController(IItemRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        [HttpGet("{board_id:guid}")]
        public async Task<Board> GetBoardById(Guid board_id, [FromQuery] Guid user_id)
        {
            var items = await _itemsRepository.GetByBoardId(user_id, board_id);
            return Board.FromData(user_id, board_id, items);
        }
    }
}
