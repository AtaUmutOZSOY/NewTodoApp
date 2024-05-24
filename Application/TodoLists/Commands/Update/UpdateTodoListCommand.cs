using Application.Common.Exceptions;
using Application.TodoLists.Commands.Delete;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoLists.Commands.Update
{
    public record UpdateTodoListCommand : IRequest<Unit>
    {
        public UpdateTodoListCommand(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }

       
    }

    public class UpdateTodoListCommandHandler: IRequestHandler<UpdateTodoListCommand, Unit>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        
        public UpdateTodoListCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = _applicationDbContext.TodoLists.FirstOrDefault(x=>x.Id ==  request.Id && x.Status == Core.Enums.EntityStatus.Active);

            if (entity == null) 
            {
                throw new NotFoundException(nameof(entity));
            }

            entity.Title = request.Title;

            _applicationDbContext.TodoLists.Update(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
