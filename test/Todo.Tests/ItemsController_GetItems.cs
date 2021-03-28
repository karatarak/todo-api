using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Todo.Controllers;
using Todo.Data;
using Xunit;

namespace Todo.Tests
{
    public class ItemsController_GetItems
    {
        [Fact]
        public async Task GetItems_Empty()
        {
            var userId = "john";
            var boardId = "shopping";

            var repository = new Mock<IItemRepository>();
            repository.Setup(r => r.GetByBoardId(userId, boardId)).Returns(Task.FromResult((IEnumerable<ItemData>) new ItemData[0]));

            var controller = new ItemsController(repository.Object);

            var response = await controller.GetItems(userId, boardId);

            Assert.Empty(response.items);
        }
    }
}
