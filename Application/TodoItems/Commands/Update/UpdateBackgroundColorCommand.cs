using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Infrastructure.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Commands.Update
{
    public record UpdateTodoItemBackgroundColorCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string NewColor { get; set; }
    }

    public class UpdateTodoItemBackgroundColorCommandHandler : IRequestHandler<UpdateTodoItemBackgroundColorCommand, IResult>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemBackgroundColorCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(UpdateTodoItemBackgroundColorCommand request, CancellationToken cancellationToken)
        {
            var exisItem = _context.TodoItems.FirstOrDefault(x => x.Id == request.Id);

            if (exisItem == null)
            {
                return new ErrorResult("Todo item not found.");
            }

            exisItem.BackgroundColor = request.NewColor;

            _context.TodoItems.Update(exisItem);
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResult("Background color updated successfully.");
        }
    }
}
