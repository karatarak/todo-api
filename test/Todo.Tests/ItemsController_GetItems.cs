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
            var userId = Guid.NewGuid();

            var repository = new Mock<IItemRepository>();
            repository.Setup(r => r.GetItemsByUserId(userId)).Returns(Task.FromResult((IList<ItemData>) new List<ItemData>()));

            var controller = new ItemsController(repository.Object);

            var response = await controller.GetItems(userId);

            Assert.Empty(response.items);
        }
    }
}
