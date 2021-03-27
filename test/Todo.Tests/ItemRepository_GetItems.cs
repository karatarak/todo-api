using System;
using System.Threading.Tasks;
using Todo.Data;
using Xunit;

namespace Todo.Tests
{
    public class ItemsRepository_GetItems
    {
        [Fact]
        public async Task GetByUserId_BoardIdNull_Returns3()
        {
            var userId = ItemsRepository.DefaultUserId;
            var repository = new ItemsRepository();

            var items = await repository.GetByUserId(userId, null);

            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async Task GetByUserId_BoardIdDefault_Returns1()
        {
            var userId = ItemsRepository.DefaultUserId;
            var repository = new ItemsRepository();

            var items = await repository.GetByUserId(userId, ItemsRepository.DefaultBoardId);

            Assert.Equal(2, items.Count);
        }
    }
}
