using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Todo.Data.Handlers;

namespace Todo.Data
{
    public class ItemDbRepository : IItemRepository
    {
        private readonly string _connectionString;

        static ItemDbRepository()
        {
            SqlMapper.AddTypeHandler(new BinaryIdHandler());
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }

        public ItemDbRepository(string connectionString)
        {
           _connectionString = connectionString;
        }

        public async Task<ItemData> GetById(string itemId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM `todo`.`item` WHERE `item_id` = @Id;";
                var data = new { Id = new BinaryId(itemId) };
                return await conn.QueryFirstOrDefaultAsync<ItemData>(query, data);
            }
        }

        public async Task<IEnumerable<ItemData>> GetByUserId(string userId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM `todo`.`item` WHERE 'owner_id' = @UserId ORDER BY `created_date`;";
                var data = new { UserId = new BinaryId(userId) };
                return await conn.QueryAsync<ItemData>(query, data);
            }
        }

        public async Task<IEnumerable<ItemData>> GetByBoardId(string userId, string boardId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM `todo`.`item` WHERE `owner_id` = @OwnerId AND `board_id` = @BoardId ORDER BY `created_date`;";
                var data = new { OwnerId = new BinaryId(userId), BoardId = new BinaryId(boardId) };
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
