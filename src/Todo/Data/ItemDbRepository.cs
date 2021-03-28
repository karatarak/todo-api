using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Todo.Data.Handlers;

namespace Todo.Data
{
    public class ItemDbRepository : IItemRepository
    {
        private readonly string _connectionString;

        static ItemDbRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }

        public ItemDbRepository(string connectionString)
        {
           _connectionString = connectionString;
        }

        public async Task<ItemData> GetById(Guid itemId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = "SELECT * FROM items WHERE item_id = @item_id;";
                var data = new { item_id = itemId };
                return await conn.QueryFirstOrDefaultAsync<ItemData>(query, data);
            }
        }

        public async Task<IEnumerable<ItemData>> GetByUserId(Guid userId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = "SELECT * FROM items WHERE owner_id = @OwnerId ORDER BY created_date;";
                var data = new { OwnerId = userId };
                return await conn.QueryAsync<ItemData>(query, data);
            }
        }

        public async Task<IEnumerable<ItemData>> GetByBoardId(Guid userId, Guid boardId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = "SELECT * FROM items WHERE owner_id = @OwnerId AND board_id = @BoardId ORDER BY created_date;";
                var data = new { OwnerId = userId, BoardId = boardId };
                return await conn.QueryAsync<ItemData>(query, data);
            }
        }

        public Task<ItemData> Insert(string userId, string boardId, string title, string description, string status, DateTime? dueDate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(string itemId, string boardId, string title, string description, string status, DateTime? dueDate)
        {
            throw new NotImplementedException();
        }
        
        public Task<bool> Delete(string itemId)
        {
            throw new NotImplementedException();
        }
    }
}
