using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Todo.Data.Handlers;

namespace Todo.Data
{
    public class ItemPostgresRepository : IItemRepository
    {
        private readonly string _connectionString;

        static ItemPostgresRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }

        public ItemPostgresRepository(string connectionString)
        {
           _connectionString = connectionString;
        }

        public async Task<ItemData> GetItemById(Guid itemId)
        {
            using (var c = new NpgsqlConnection(_connectionString))
            {
                var query = "SELECT * FROM items WHERE item_id = @item_id;";

                return await c.QueryFirstOrDefaultAsync<ItemData>(query, new { item_id = itemId });
            }
        }

        public async Task<IList<ItemData>> GetItemsByUserId(Guid userId)
        {
            using (var c = new NpgsqlConnection(_connectionString))
            {
                var query = @"
                    SELECT * FROM items
                    WHERE owner_id = @owner_id
                    ORDER BY created_date;";

                var results = await c.QueryAsync<ItemData>(query, new { owner_id = userId });
                return results.ToList();
            }
        }

        public async Task<ItemData> CreateItem(Guid userId, Guid boardId, string title, string description, string status, DateTime? dueDate)
        {
            using (var c = new NpgsqlConnection(_connectionString))
            {
                var item = new ItemData
                {
                    item_id = Guid.NewGuid(),
                    owner_id = userId,
                    board_id = boardId,
                    title = title.Trim(),
                    description = description.Trim(),
                    status = status.ToLowerInvariant().Trim(),
                    created_date = DateTime.UtcNow,
                    due_date = dueDate,
                };

                var query = @"
                    INSERT INTO items (item_id, owner_id, board_id, title, description, status, created_date, due_date) 
                    VALUES (@item_id, @owner_id, @board_id, @title, @description, @status, @created_date, @due_date);";

                await c.ExecuteAsync(query, item);

                return item;
            }
        }

        public async Task<bool> UpdateItem(Guid itemId, string title, string description, string status, DateTime? dueDate)
        {
            using (var c = new NpgsqlConnection(_connectionString))
            {
                var data = new
                {
                    item_id = itemId,
                    title = title.Trim(),
                    description = description.Trim(),
                    status = status.ToLowerInvariant().Trim(),
                    due_date = dueDate,
                };

                var query = @"
                    UPDATE items SET title = @title, description = @description, status = @status, due_date = @due_date
                    WHERE item_id = @item_id;";

                return await c.ExecuteAsync(query, data) == 1;
            }
        }
        
        public async Task<bool> DeleteItem(Guid itemId)
        {
            using (var c = new NpgsqlConnection(_connectionString))
            {
                var query = "DELETE FROM items WHERE item_id = @item_id;";

                return await c.ExecuteAsync(query, new { item_id = itemId }) == 1;
            }
        }
    }
}
