using Application.TodoItems.Commands.Delete;
using Application.TodoItems.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ApiControllerBase
    {
        [HttpPost("createTodoItem")]

        public async Task<ActionResult> CreateTodoItem(CreateTodoItemCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getAllActiveTodoItems")]

        public async Task<ActionResult> GetActiveTodoItemsByListId([FromQuery]GetAllTodoItemsByListIdQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("deleteById/{id}")]
        public async Task<ActionResult> DeleteTodoItem(int id)
        {
            var deleteTodoItemCommand = new DeleteTodoItemCommand { Id = id };
            var result = await Mediator.Send(deleteTodoItemCommand);

            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }


    }
}
