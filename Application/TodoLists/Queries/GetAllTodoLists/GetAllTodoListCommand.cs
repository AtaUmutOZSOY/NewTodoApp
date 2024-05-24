using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace Application.TodoLists.Queries.GetAllTodoLists
{
    public record GetAllTodoListsQuery : IRequest<IDataResult<List<TodoList>>>
    {
    }

    public class GetAllTodoListsQueryHandler : IRequestHandler<GetAllTodoListsQuery, IDataResult<List<TodoList>>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllTodoListsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IDataResult<List<TodoList>>> Handle(GetAllTodoListsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var todoLists = await _context.TodoLists
                    .Where(t => t.Status != Core.Enums.EntityStatus.Inactive) 
                    .ToListAsync(cancellationToken);

                return new SuccessDataResult<List<TodoList>>(todoLists, "Todo lists retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<TodoList>>("An error occurred while retrieving todo lists: " + ex.Message);
            }
        }
    }
}
