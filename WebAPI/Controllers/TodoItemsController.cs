using Application.TodoItems.Commands.Delete;
using Application.TodoItems.Commands.Update;
using Application.TodoItems.Commands.UpdateNote;
using Application.TodoItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createTodoItem")]
        public async Task<ActionResult> CreateTodoItem(CreateTodoItemCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getAllActiveTodoItems")]
        public async Task<ActionResult> GetActiveTodoItemsByListId([FromQuery] GetAllTodoItemsByListIdQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("deleteById/{id}")]
        public async Task<ActionResult> DeleteTodoItem(int id)
        {
            var deleteTodoItemCommand = new DeleteTodoItemCommand { Id = id };
            var result = await _mediator.Send(deleteTodoItemCommand);

            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPut("updateTodoItem")]
        public async Task<ActionResult> UpdateTodoItemNote(UpdateTodoItemNoteCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("markAsCompleted/{id}")]
        public async Task<ActionResult> MarkAsCompleted(int id)
        {
            var markAsCompletedCommand = new MarkAsCompletedCommand { Id = id };
            var result = await _mediator.Send(markAsCompletedCommand);

            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPut("updateTodoItemBackgroundColor")]
        public async Task<ActionResult> UpdateTodoItemBackgroundColor(UpdateTodoItemBackgroundColorCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
