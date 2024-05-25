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

namespace Application.TodoItemTags.Commands.Create
{
    public record CreateTodoItemTagCommand:IRequest<IResult>
    {
        public int TodoItemId { get; set; }

        public List<string> Tags { get; set; }
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
            foreach (var tag in request.Tags)
            {
                var existItemTag = await _context.TodoItemTags.FirstOrDefaultAsync(x => x.TodoItemId == request.TodoItemId && x.Tag == tag);
                if (existItemTag != null)
                {
                    return new ErrorResult("This tag already exist");
                }
            }
           

            foreach (var tag in request.Tags)
            {
                var newTag = new TodoItemTag()
                {
                    Tag = tag,
                    TodoItemId = request.TodoItemId,

                };
                await _context.TodoItemTags.AddAsync(newTag);
            }

           

            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResult();

        }
    }
}
