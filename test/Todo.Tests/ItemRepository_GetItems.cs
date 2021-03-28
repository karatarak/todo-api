using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Todo.Controllers;
using Todo.Data;
using Xunit;

namespace Todo.Tests
{
    public class ItemsRepository_GetItems
    {
        [Fact]
        public async Task GetByUserId_BoardIdNull_Returns8()
        {
            var userId = Guid.NewGuid();
            var boardId = Guid.NewGuid();

            var repository = new Mock<IItemRepository>();
            repository.Setup(r => r.GetByBoardId(userId, boardId)).Returns(Task.FromResult((IEnumerable<ItemData>) new ItemData[0]));

            var controller = new ItemsController(repository.Object);

            var response = await controller.GetItems(userId, boardId);

            Assert.Empty(response.items);
        }
    }
}
