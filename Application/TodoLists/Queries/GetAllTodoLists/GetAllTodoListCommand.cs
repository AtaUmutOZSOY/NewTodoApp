using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.TodoLists.Queries.GetAllTodoLists
{
    public record GetAllTodoListsQuery : IRequest<List<TodoList>>
    {
    }
    public class GetAllTodoListsQueryHandler : IRequestHandler<GetAllTodoListsQuery, List<TodoList>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllTodoListsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoList>> Handle(GetAllTodoListsQuery request, CancellationToken cancellationToken)
        {
            return await _context.TodoLists.ToListAsync(cancellationToken);
        }
    }
}
