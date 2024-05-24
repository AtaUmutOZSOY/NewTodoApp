using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.TodoItems.Commands.Create;
using Application.TodoLists.Commands.Delete;
using Application.TodoLists.Commands.Create;
using Application.TodoLists.Queries.GetAllTodoLists;
using Domain.Entities;
using Application.TodoLists.Commands.Update;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListsController : ApiControllerBase
    {
        [HttpGet("getAllTodoLists")]
        public async Task<ActionResult<List<TodoList>>> GetAll()
        {
            var result = await Mediator.Send(new GetAllTodoListsQuery());
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("createTodoList")]
        public async Task<ActionResult<int>> Create(CreateTodoListCommand createTodoListCommand)
        {
            var result = await Mediator.Send(createTodoListCommand);
            
            if (result.Success)
            {
                return Ok(result);    
            }
            return BadRequest(result);
        }

        [HttpPut("deleteById/{id}")]

        public async Task<ActionResult> SoftDelete(int id)
        {
           var result =  await Mediator.Send(new SoftDeleteTodoListCommand(id));

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("updateTodoListName")]

        public async Task<ActionResult> Update(UpdateTodoListNameCommand updateTodoListCommand)
        {
           var result = await Mediator.Send(updateTodoListCommand);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
