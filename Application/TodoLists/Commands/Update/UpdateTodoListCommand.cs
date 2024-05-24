using Application.Common.Exceptions;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete; 

namespace Application.TodoLists.Commands.Update
{
    public record UpdateTodoListNameCommand : IRequest<IResult>
    {
        public UpdateTodoListNameCommand(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListNameCommand, IResult>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UpdateTodoListCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IResult> Handle(UpdateTodoListNameCommand request, CancellationToken cancellationToken)
        {
            var entity = _applicationDbContext.TodoLists.FirstOrDefault(x => x.Id == request.Id && x.Status == Core.Enums.EntityStatus.Active);

            if (entity == null)
            {
                return new ErrorResult("Todo list not found.");
            }

            entity.Title = request.Title;

            _applicationDbContext.TodoLists.Update(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new SuccessResult("Todo list updated successfully.");
        }
    }
}
