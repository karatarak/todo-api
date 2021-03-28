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
            SqlMapper.AddTypeHandler(new DateTimeHandler());
            SqlMapper.AddTypeHandler(new UuidHandler());
        }

        public ItemDbRepository(string connectionString)
        {
           _connectionString = connectionString;
        }

        public async Task<ItemData> GetById(Guid itemId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM `todo`.`item` WHERE `item_id` = @Id;";
                var data = new { Id = new Uuid(itemId) };
                return await conn.QueryFirstOrDefaultAsync<ItemData>(query, data);
            }
        }

        public async Task<IEnumerable<ItemData>> GetByUserId(Guid userId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM `todo`.`item` WHERE 'owner_id' = @UserId ORDER BY `created_date`;";
                var data = new { UserId = new Uuid(userId) };
                return await conn.QueryAsync<ItemData>(query, data);
            }
        }

        public async Task<IEnumerable<ItemData>> GetByBoardId(Guid userId, Guid boardId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                //var query = "SELECT * FROM `todo`.`item` WHERE `owner_id` = @UserId AND `board_id` = @BoardId ORDER BY `created_date`;";
                var query = "SELECT * FROM `todo`.`item` ORDER BY `created_date`;";
                var data = new { UserId = new Uuid(userId), BoardId = new Uuid(boardId) };
                return await conn.QueryAsync<ItemData>(query, data);
            }
        }

        public Task<ItemData> Insert(string userId, string boardId, string title, string description, string status, DateTime? dueDate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid itemId, string boardId, string title, string description, string status, DateTime? dueDate)
        {
            throw new NotImplementedException();
        }
        
        public Task<bool> Delete(Guid itemId)
        {
            throw new NotImplementedException();
        }
    }
}
