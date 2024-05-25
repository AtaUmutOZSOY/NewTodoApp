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

namespace Application.TodoItems.Commands.Delete
{
    public record DeleteTodoItemCommand:IRequest<IResult>
    {
        public int Id { get; set; }
    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, IResult>
    {
        private IApplicationDbContext _applicationDbContext;

        public DeleteTodoItemCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IResult> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var existEntity = await _applicationDbContext.TodoItems.FirstOrDefaultAsync(x => x.Id == request.Id && x.Status == Core.Enums.EntityStatus.Active);

            if (existEntity is null)
            {
                return new ErrorResult("Todo item not found.");
            }

            existEntity.Status = Core.Enums.EntityStatus.Inactive;

            _applicationDbContext.TodoItems.Update(existEntity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new SuccessResult("Todo item deleted.");
        }
    }
}
