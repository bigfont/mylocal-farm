using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using farmApi.Models;
using farmApi.Controllers;
using farmApi.DAL.Interfaces;

namespace farmApi.Test
{
    public class TodoControllerTests
    {
        [Fact]
        public void GetAll_GetsCollection()
        {
            // we're connecting the repository to a list
            // not to a dbContext.

            // setup mock repository
            var all = new List<TodoItem>()
            {
                new TodoItem(),
                new TodoItem(),
                new TodoItem()
            };            

            var mockRepo = new Mock<IGenericRepository<TodoItem>>();
            mockRepo.Setup(x => x.All()).Returns(all);

            // setup mock unit or work
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.TodoItemRepository).Returns(mockRepo.Object);

            // test controller
            TodoController controller = new TodoController(mockUnitOfWork.Object);
            var result = controller.GetAll();

            Assert.Equal(3, result.Count());
        }
    }
}

