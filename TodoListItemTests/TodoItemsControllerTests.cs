using Application.TodoItems.Commands.Delete;
using Application.TodoItems.Commands.Update;
using Application.TodoItems.Commands.UpdateNote;
using Application.TodoItems.Queries;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WebAPI.Controllers;
using MediatR;
using Core.Utilities.Results;
using System.Collections.Generic;
using Core.Utilities.Results.Concrete;
using Domain.Entities;

namespace WebAPI.Tests
{
    [TestFixture]
    public class TodoItemsControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private TodoItemsController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new TodoItemsController(_mediatorMock.Object);
        }

        [Test]
        public async Task CreateTodoItem_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var command = new CreateTodoItemCommand { Title = "Test Todo" };
            var result = new SuccessResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.CreateTodoItem(command);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task CreateTodoItem_ShouldReturnBadRequestResult_WhenFailed()
        {
            // Arrange
            var command = new CreateTodoItemCommand { Title = "Test Todo" };
            var result = new ErrorResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.CreateTodoItem(command);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public async Task GetActiveTodoItemsByListId_ShouldReturnOkResult()
        {
            // Arrange
            var query = new GetAllTodoItemsByListIdQuery { ListId = 1 };
            var result = new SuccessDataResult<List<TodoItem>>(new List<TodoItem>());
            _mediatorMock.Setup(m => m.Send(query, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.GetActiveTodoItemsByListId(query);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task DeleteTodoItem_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var command = new DeleteTodoItemCommand { Id = 1 };
            var result = new SuccessResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.DeleteTodoItem(command.Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task DeleteTodoItem_ShouldReturnNotFoundResult_WhenFailed()
        {
            // Arrange
            var command = new DeleteTodoItemCommand { Id = 1 };
            var result = new ErrorResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.DeleteTodoItem(command.Id);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(response);
        }

        [Test]
        public async Task UpdateTodoItemNote_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var command = new UpdateTodoItemNoteCommand { Id = 1, Note = "New Note" };
            var result = new SuccessResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.UpdateTodoItemNote(command);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task UpdateTodoItemNote_ShouldReturnBadRequestResult_WhenFailed()
        {
            // Arrange
            var command = new UpdateTodoItemNoteCommand { Id = 1, Note = "New Note" };
            var result = new ErrorResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.UpdateTodoItemNote(command);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public async Task MarkAsCompleted_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var command = new MarkAsCompletedCommand { Id = 1 };
            var result = new SuccessResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.MarkAsCompleted(command.Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task MarkAsCompleted_ShouldReturnNotFoundResult_WhenFailed()
        {
            // Arrange
            var command = new MarkAsCompletedCommand { Id = 1 };
            var result = new ErrorResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.MarkAsCompleted(command.Id);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(response);
        }

        [Test]
        public async Task UpdateTodoItemBackgroundColor_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var command = new UpdateTodoItemBackgroundColorCommand { Id = 1, NewColor = "#FFFFFF" };
            var result = new SuccessResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.UpdateTodoItemBackgroundColor(command);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task UpdateTodoItemBackgroundColor_ShouldReturnBadRequestResult_WhenFailed()
        {
            // Arrange
            var command = new UpdateTodoItemBackgroundColorCommand { Id = 1, NewColor = "#FFFFFF" };
            var result = new ErrorResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.UpdateTodoItemBackgroundColor(command);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }
    }
}
