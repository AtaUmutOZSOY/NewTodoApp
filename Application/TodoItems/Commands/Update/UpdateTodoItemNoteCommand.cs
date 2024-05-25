using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Commands.UpdateNote
{
    public record UpdateTodoItemNoteCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Note { get; set; }
    }

    public class UpdateTodoItemNoteCommandHandler : IRequestHandler<UpdateTodoItemNoteCommand, IResult>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemNoteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(UpdateTodoItemNoteCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(ti => ti.Id == request.Id && ti.Status == Core.Enums.EntityStatus.Active, cancellationToken);
            if (todoItem == null)
            {
                return new ErrorResult("Todo item not found.");
            }

            todoItem.Note = request.Note;
            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResult("Todo item note updated successfully.");
        }
    }
}
