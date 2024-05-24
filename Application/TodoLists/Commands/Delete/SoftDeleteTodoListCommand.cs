using Application.Common.Exceptions;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoLists.Commands.Delete
{
    public record SoftDeleteTodoListCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public SoftDeleteTodoListCommand(int id)
        {
            Id = id;
        }
    }

    public class SoftDeleteTodoListCommandHandler : IRequestHandler<SoftDeleteTodoListCommand, IResult>
    {
        private readonly ApplicationDbContext _context;

        public SoftDeleteTodoListCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(SoftDeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoLists.FindAsync(request.Id);

            if (entity == null)
            {
                return new ErrorResult("Todo list not found.");
            }

            entity.Status = Core.Enums.EntityStatus.Inactive;
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResult("Todo list soft-deleted successfully.");
        }
    }
}
