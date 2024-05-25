using Application.TodoLists.Commands.Create;
using Application.TodoLists.Commands.Delete;
using Application.TodoLists.Commands.Update;
using Application.TodoLists.Queries.GetAllTodoLists;
using Core.Utilities.Results;
using Core.Utilities.Results.Concrete;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests
{
    [TestFixture]
    public class TodoListsControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private TodoListsController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new TodoListsController(_mediatorMock.Object);
        }

        [Test]
        public async Task GetAll_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var result = new SuccessDataResult<List<TodoList>>(new List<TodoList>());
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllTodoListsQuery>(), default)).ReturnsAsync(result);

            // Act
            var response = await _controller.GetAll();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
        }

        [Test]
        public async Task GetAll_ShouldReturnBadRequest_WhenFailed()
        {
            // Arrange
            var result = new ErrorDataResult<List<TodoList>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllTodoListsQuery>(), default)).ReturnsAsync(result);

            // Act
            var response = await _controller.GetAll();

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response.Result);
        }

        [Test]
        public async Task Create_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var command = new CreateTodoListCommand { Title = "Test List" };
            var result = new SuccessDataResult<int>(1);
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.Create(command);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
        }

        [Test]
        public async Task Create_ShouldReturnBadRequest_WhenFailed()
        {
            // Arrange
            var command = new CreateTodoListCommand { Title = "Test List" };
            var result = new ErrorDataResult<int>(); 
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.Create(command);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response.Result);
        }

        [Test]
        public async Task SoftDelete_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var command = new SoftDeleteTodoListCommand(1);
            var result = new SuccessResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.SoftDelete(command.Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task SoftDelete_ShouldReturnBadRequest_WhenFailed()
        {
            // Arrange
            var command = new SoftDeleteTodoListCommand(1);
            var result = new ErrorResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.SoftDelete(command.Id);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public async Task Update_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            var command = new UpdateTodoListNameCommand(1, "Updated Title");
            var result = new SuccessResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.Update(command);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(response);
        }

        [Test]
        public async Task Update_ShouldReturnBadRequest_WhenFailed()
        {
            // Arrange
            var command = new UpdateTodoListNameCommand(1, "Updated Title");
            var result = new ErrorResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            // Act
            var response = await _controller.Update(command);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }
    }
}
