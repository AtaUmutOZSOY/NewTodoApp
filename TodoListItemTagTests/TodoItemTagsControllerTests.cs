
using Application.TodoItems.Queries;
using Application.TodoItemTags.Commands.Create;
using Application.TodoItemTags.Commands.Delete;
using Application.TodoItemTags.Commands.Remove;
using Application.TodoItemTags.Queries.GetTodoItemTags;
using Core.Utilities.Results.Concrete;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace WebAPI.Tests
{
    [TestFixture]
    public class TodoItemTagsControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private TodoItemTagsController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new TodoItemTagsController(_mediatorMock.Object);
        }

        [Test]
        public async Task Create_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var command = new CreateTodoItemTagCommand { TodoItemId = 1, Tag = "Test Tag" };
            var result = new SuccessResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.Create(command);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task Create_ShouldReturnBadRequestResult_WhenFailed()
        {
            // Arrange
            var command = new CreateTodoItemTagCommand { TodoItemId = 1, Tag = "Test Tag" };
            var result = new ErrorResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.Create(command);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public async Task DeleteTodoItemTag_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var command = new DeleteTodoItemTagCommand { Id = 1 };
            var result = new SuccessResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.DeleteTodoItemTag(command.Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task DeleteTodoItemTag_ShouldReturnBadRequestResult_WhenFailed()
        {
            // Arrange
            var command = new DeleteTodoItemTagCommand { Id = 1 };
            var result = new ErrorResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.DeleteTodoItemTag(command.Id);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public async Task RemoveTodoItemTag_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var command = new RemoveTodoItemTagFromTodoItemCommand { Id = 1 };
            var result = new SuccessResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.RemoveTodoItemTag(command.Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task RemoveTodoItemTag_ShouldReturnBadRequestResult_WhenFailed()
        {
            // Arrange
            var command = new RemoveTodoItemTagFromTodoItemCommand { Id = 1 };
            var result = new ErrorResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.RemoveTodoItemTag(command.Id);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public async Task GetAllTodoItemTagsByTodoItemId_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var query = new GetTodoItemTagsCommand { TodoItemId = 1 };
            var result = new SuccessDataResult<List<TodoItemTag>>(new List<TodoItemTag>());
            _mediatorMock.Setup(m => m.Send(query, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.GetAllTodoItemTagsByTodoItemId(query.TodoItemId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task GetAllTodoItemTagsByTodoItemId_ShouldReturnNotFoundResult_WhenFailed()
        {
            // Arrange
            var query = new GetTodoItemTagsCommand { TodoItemId = 1 };
            var result = new ErrorDataResult<List<TodoItemTag>>();
            _mediatorMock.Setup(m => m.Send(query, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.GetAllTodoItemTagsByTodoItemId(query.TodoItemId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(response);
        }

       
       
    }
}
