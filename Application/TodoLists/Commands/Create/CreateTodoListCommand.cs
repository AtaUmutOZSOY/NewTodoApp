using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoLists.Commands.Create
{
    public record CreateTodoListCommand : IRequest<IDataResult<int>>
    {
        public string Title { get; init; }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, IDataResult<int>>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IDataResult<int>> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new TodoList
                {
                    Title = request.Title
                };

                await _context.TodoLists.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return new SuccessDataResult<int>(entity.Id, "Todo list created successfully.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<int>("An error occurred while creating the todo list: " + ex.Message);
            }
        }
    }
}
