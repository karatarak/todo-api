using System;
using System.Threading.Tasks;
using Todo.Data;
using Xunit;

namespace Todo.Tests
{
    public class ItemsRepository_GetItems
    {
        [Fact]
        public async Task GetByUserId_BoardIdNull_Returns8()
        {
            var userId = ItemsRepository.DefaultUserId;
            var repository = new ItemsRepository();

            var items = await repository.GetByUserId(userId, null);

            Assert.Equal(8, items.Count);
        }

        [Fact]
        public async Task GetByUserId_BoardIdDefault_Returns3()
        {
            var userId = ItemsRepository.DefaultUserId;
            var repository = new ItemsRepository();

            var items = await repository.GetByUserId(userId, ItemsRepository.DefaultBoardId);

            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async Task GetByUserId_BoardIdNull_DueDateAscending()
        {
            var userId = ItemsRepository.DefaultUserId;
            var repository = new ItemsRepository();

            var items = await repository.GetByUserId(userId, null);

            Assert.True(items[0].due_date < items[1].due_date);
        }
    }
}
