﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IMediator _mediator;

        public TodoListsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getAllTodoLists")]
        public async Task<ActionResult<List<TodoList>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllTodoListsQuery());
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("createTodoList")]
        public async Task<ActionResult<int>> Create(CreateTodoListCommand createTodoListCommand)
        {
            var result = await _mediator.Send(createTodoListCommand);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("deleteById/{id}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            var result = await _mediator.Send(new SoftDeleteTodoListCommand(id));

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("updateTodoListName")]
        public async Task<ActionResult> Update(UpdateTodoListNameCommand updateTodoListCommand)
        {
            var result = await _mediator.Send(updateTodoListCommand);

            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result);
        }
    }
}
