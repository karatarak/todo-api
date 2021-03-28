using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Todo.Data.Handlers;

namespace Todo.Data
{
    public class BoardPostgresRepository : IBoardRepository
    {
        private readonly string _connectionString;

        public BoardPostgresRepository(string connectionString)
        {
           _connectionString = connectionString;
        }

        public async Task<BoardData> CreateBoard(Guid userId, string title)
        {
            using (var c = new NpgsqlConnection(_connectionString))
            {
                var board = new BoardData
                {
                    board_id = Guid.NewGuid(),
                    owner_id = userId,
                    title = title.Trim(),
                    created_date = DateTime.UtcNow,
                };

                var query = @"
                    INSERT INTO boards (board_id, owner_id, title, created_date) 
                    VALUES (@board_id, @owner_id, @title, @created_date);";

                await c.ExecuteAsync(query, board);

                return board;
            }
        }

        public async Task<bool> UpdateBoard(Guid boardId, string title)
        {
            using (var c = new NpgsqlConnection(_connectionString))
            {
                var data = new
                {
                    board_id = boardId,
                    title = title.Trim(),
                };

                var query = @"
                    UPDATE boards SET title = @title
                    WHERE board_id = @board_id;";

                return await c.ExecuteAsync(query, data) == 1;
            }
        }

        public async Task<bool> DeleteBoard(Guid boardId)
        {
            using (var c = new NpgsqlConnection(_connectionString))
            {
                var query = "DELETE FROM boards WHERE board_id = @board_id;";

                return await c.ExecuteAsync(query, new { board_id = boardId }) == 1;
            }
        }

        public async Task<BoardData> GetBoardById(Guid boardId)
        {
            using (var c = new NpgsqlConnection(_connectionString))
            {
                var query = "SELECT * FROM boards WHERE board_id = @board_id;";

                return await c.QueryFirstOrDefaultAsync<BoardData>(query, new { board_id = boardId });
            }
        }

        public async Task<BoardItemsData> GetBoardItemsById(Guid boardId)
        {
            using (var c = new NpgsqlConnection(_connectionString))
            {
                var query1 = @"SELECT * FROM boards WHERE board_id = @board_id;";

                var query2 = @"
                    SELECT * FROM items
                    WHERE board_id = @board_id
                    ORDER BY created_date;";
   
                using (var m = await c.QueryMultipleAsync($"{query1} {query2}", new { board_id = boardId }))
                {
                    var board = await m.ReadSingleAsync<BoardData>();
                    var items = await m.ReadAsync<ItemData>();

                    return new BoardItemsData
                    {
                        board = board,
                        items = items.ToArray(),
                    };
                }
            }
        }
    }
}
