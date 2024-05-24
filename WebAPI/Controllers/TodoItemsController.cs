using Application.TodoItems.Commands.Create;
using Application.TodoItems.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<ActionResult> GetActiveTodoItemsByListId([FromQuery]GetAllListItemsByListIdQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}
