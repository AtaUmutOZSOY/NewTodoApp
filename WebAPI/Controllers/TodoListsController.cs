using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.TodoItems.Commands.Create;
using Application.TodoLists.Commands.Delete;
using Application.TodoLists.Commands.Create;
using Application.TodoLists.Queries.GetAllTodoLists;
using Domain.Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListsController : ApiControllerBase
    {
        [HttpGet("getAllTodoList")]
        public async Task<ActionResult<List<TodoList>>> GetAll()
        {
            return await Mediator.Send(new GetAllTodoListsQuery());
        }
        [HttpPost("createTodoList")]
        public async Task<ActionResult<int>> Create(CreateTodoListCommand createTodoListCommand)
        {
            return await Mediator.Send(createTodoListCommand);
        }

        [HttpPut("deleteById/{id}")]

        public async Task<ActionResult> SoftDelete(int id)
        {
            await Mediator.Send(new SoftDeleteTodoListCommand(id));
            
            return NoContent();
        }
    }
}
