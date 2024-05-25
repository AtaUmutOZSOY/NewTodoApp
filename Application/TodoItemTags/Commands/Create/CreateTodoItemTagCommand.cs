using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItemTags.Commands.Create
{
    public record CreateTodoItemTagCommand : IRequest<IResult>
    {
        public int TodoItemId { get; set; }
        public string Tag { get; set; }
    }

    public class CreateTodoItemTagCommandHandler : IRequestHandler<CreateTodoItemTagCommand, IResult>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemTagCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(CreateTodoItemTagCommand request, CancellationToken cancellationToken)
        {
            var existItemTag = await _context.TodoItemTags
                .FirstOrDefaultAsync(x => x.TodoItemId == request.TodoItemId && x.Tag == request.Tag);

            if (existItemTag != null)
            {
                return new ErrorResult("This tag already exists");
            }

            var newTag = new TodoItemTag()
            {
                Tag = request.Tag,
                TodoItemId = request.TodoItemId,
            };

            await _context.TodoItemTags.AddAsync(newTag);
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResult();
        }
    }
}
