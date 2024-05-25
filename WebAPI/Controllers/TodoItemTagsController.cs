using Application.TodoItems.Queries;
using Application.TodoItemTags.Commands.Create;
using Application.TodoItemTags.Commands.Delete;
using Application.TodoItemTags.Commands.Remove;
using Application.TodoItemTags.Queries.GetTodoItemTags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemTagsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public TodoItemTagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createTodoItemTags")]
        public async Task<ActionResult> Create(CreateTodoItemTagCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("deleteTodoItemTag/{id}")]
        public async Task<ActionResult> DeleteTodoItemTag(int id)
        {
            var command = new DeleteTodoItemTagCommand { Id = id };
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("removeTodoItemTagFromTodoItem/{id}")]
        public async Task<ActionResult> RemoveTodoItemTag(int id)
        {
            var command = new RemoveTodoItemTagFromTodoItemCommand { Id = id };
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getAllTodoItemTagsByTodoItemId/{todoItemId}")]
        public async Task<ActionResult> GetAllTodoItemTagsByTodoItemId(int todoItemId)
        {
            var command = new GetTodoItemTagsCommand { TodoItemId = todoItemId };
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("getTagCounts")]
        public async Task<ActionResult> GetTagCounts([FromQuery] int listId)
        {
            var result = await _mediator.Send(new GetTagCountsQuery { ListId = listId });

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
