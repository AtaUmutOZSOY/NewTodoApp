using Application.TodoLists.Queries.GetAllTodoLists;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoItems.Queries
{
    public record GetAllTodoItemsByListIdQuery: IRequest<IDataResult<List<TodoItem>>>
    {
        [Required]
        public int ListId { get; set; }
    }

    public class GetAllListItemsByListIdQueryHandler : IRequestHandler<GetAllTodoItemsByListIdQuery, IDataResult<List<TodoItem>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllListItemsByListIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IDataResult<List<TodoItem>>> Handle(GetAllTodoItemsByListIdQuery request, CancellationToken cancellationToken)
        {
            var toDoItems = await _applicationDbContext.TodoItems.Where(x=>x.ListId == request.ListId && x.Status == Core.Enums.EntityStatus.Active).ToListAsync();

            return new SuccessDataResult<List<TodoItem>>(toDoItems);

        }
    }
}
