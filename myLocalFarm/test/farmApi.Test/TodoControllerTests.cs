using System.Collections.Generic;
using System.Linq;
using System;

using Xunit;
using Moq;
using farmApi.Models;
using farmApi.Controllers;
using farmApi.DAL.Interfaces;
using Microsoft.Framework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Net.Http.Server;
using Microsoft.AspNet.Http.Core;

namespace farmApi.Test
{
    public class TodoControllerTests
    {
        private const int ItemsToCreate = 10;
        private IUnitOfWork unitOfWork;
        private List<TodoItem> todoItems;

        public TodoControllerTests()
        {
            var mockRepo = CreateMockTodoItemRepository();

            // setup mock unit of work
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.TodoItemRepository)
                .Returns(mockRepo);

            this.unitOfWork = mockUnitOfWork.Object;
        }

        [Fact]
        public void GetAll_GetsAllTodoItems()
        {
            var controller = new TodoController(this.unitOfWork);
            var result = controller.GetAll();
            Assert.Equal(ItemsToCreate, result.Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetTodoItemById(int id)
        {
            var controller = new TodoController(this.unitOfWork);
            var result = controller.GetById(id) as ObjectResult;
            var todoItem = result.Value as TodoItem;
            Assert.Equal(id, todoItem.Id);
        }

        [Theory]
        [InlineData(20)]
        public void CreateTodoItem(int id)
        {
            var controller = new TodoController(this.unitOfWork);

            var context = new DefaultHttpContext();
            controller.ActionContext.HttpContext = context;

            var createMe = new TodoItem { Id = id };
            controller.CreateTodoItem(createMe);

            var created = controller.GetById(id) as ObjectResult;
            Assert.Equal(id, (created.Value as TodoItem).Id);

            var statusCode = context.Response.StatusCode;
            Assert.Equal(201, statusCode);
        }

        [Fact]
        public void DeleteItem()
        {
            var controller = new TodoController(this.unitOfWork);

            var all = controller.GetAll();
            var initialCount = all.Count();
            var deleteMe = all.FirstOrDefault();

            controller.DeleteItem(deleteMe.Id);

            var finalCount = controller.GetAll().Count();
            Assert.Equal(1, initialCount - finalCount);
        }

        private IGenericRepository<TodoItem> CreateMockTodoItemRepository()
        {
            // add items
            this.todoItems = new List<TodoItem>();
            for (var i = 0; i < ItemsToCreate; ++i)
            {
                todoItems.Add(new TodoItem { Id = i });
            }

            // create repo implementation
            var mockRepo = new Mock<IGenericRepository<TodoItem>>();

            // All()
            mockRepo
                .Setup(x => x.All())
                .Returns(todoItems);

            // GetById()
            mockRepo
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns<int>(id =>
                {
                    return todoItems.First(item => item.Id == id);
                });

            // CreateTodoItem
            mockRepo
                .Setup(x => x.Add(It.IsAny<TodoItem>()))
                .Callback<TodoItem>(item =>
                {
                    todoItems.Add(item);
                });

            // TryDelete()
            mockRepo
                .Setup(x => x.TryDelete(It.IsAny<int>()))
                .Callback<int>(id =>
                {
                    var deleteMe = todoItems.First(item => item.Id == id);
                    todoItems.Remove(deleteMe);
                })
                .Returns(true);

            return mockRepo.Object;
        }
    }
}

