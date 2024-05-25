using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoItemTags.Commands.Remove
{
    public record RemoveTodoItemTagFromTodoItemCommand:IRequest<IResult>
    {
        public int Id { get; set; }
    }

    public class RemoveTodoItemTagFromTodoItemCommandHandler : IRequestHandler<RemoveTodoItemTagFromTodoItemCommand, IResult>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public RemoveTodoItemTagFromTodoItemCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IResult> Handle(RemoveTodoItemTagFromTodoItemCommand request, CancellationToken cancellationToken)
        {
            var existTodoItemTag = await _applicationDbContext.TodoItemTags.FirstOrDefaultAsync(x=>x.Id == request.Id);

            if (existTodoItemTag == null) 
            {
                return new ErrorResult("Todo item tag not found");
            }

            existTodoItemTag.TodoItemId = 0;

            _applicationDbContext.TodoItemTags.Update(existTodoItemTag);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new SuccessResult();
        }
    }
}
