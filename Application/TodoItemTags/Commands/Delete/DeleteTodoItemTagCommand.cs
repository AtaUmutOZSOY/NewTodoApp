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

namespace Application.TodoItemTags.Commands.Delete
{
    public record DeleteTodoItemTagCommand:IRequest<IResult>
    {
        public int Id { get; set; }
    }
    public class DeleteTodoItemTagCommandHandler : IRequestHandler<DeleteTodoItemTagCommand, IResult>
    {  
        private readonly IApplicationDbContext _context;

        public DeleteTodoItemTagCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(DeleteTodoItemTagCommand request, CancellationToken cancellationToken)
        {
            var existTag =await  _context.TodoItemTags.FirstOrDefaultAsync(t => t.Id == request.Id);

            if (existTag == null) 
            {
                return new ErrorResult("Todo item tag not found");
            }

            existTag.Status = Core.Enums.EntityStatus.Inactive;
            _context.TodoItemTags.Update(existTag);
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResult();
        }
    }
}
