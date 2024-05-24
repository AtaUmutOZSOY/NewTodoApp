using Application.TodoLists.Queries.GetAllTodoLists;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoItems.Queries
{
    public record GetAllListItemsByListIdQuery: IRequest<IDataResult<List<TodoItem>>>
    {
        public int ListId { get; set; }
    }

    public class GetAllListItemsByListIdQueryHandler : IRequestHandler<GetAllListItemsByListIdQuery, IDataResult<List<TodoItem>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllListItemsByListIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IDataResult<List<TodoItem>>> Handle(GetAllListItemsByListIdQuery request, CancellationToken cancellationToken)
        {
            var toDoItems = await _applicationDbContext.TodoItems.Where(x=>x.ListId == request.ListId && x.Status == Core.Enums.EntityStatus.Active).ToListAsync();

            return new SuccessDataResult<List<TodoItem>>(toDoItems);

        }
    }
}
