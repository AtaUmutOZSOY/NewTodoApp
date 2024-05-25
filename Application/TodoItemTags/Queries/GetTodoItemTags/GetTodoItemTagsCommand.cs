using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItemTags.Queries.GetTodoItemTags
{
    public record GetTodoItemTagsCommand : IRequest<IDataResult<List<TodoItemTag>>>
    {
        public int TodoItemId { get; set; }
    }

    public class GetTodoItemTagsCommandHandler : IRequestHandler<GetTodoItemTagsCommand, IDataResult<List<TodoItemTag>>>
    {
        private readonly IApplicationDbContext _context;

        public GetTodoItemTagsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IDataResult<List<TodoItemTag>>> Handle(GetTodoItemTagsCommand request, CancellationToken cancellationToken)
        {
            var tags = await _context.TodoItemTags.Where(x => x.TodoItemId == request.TodoItemId).ToListAsync(cancellationToken);

            return new SuccessDataResult<List<TodoItemTag>>(tags);
        }
    }
}
