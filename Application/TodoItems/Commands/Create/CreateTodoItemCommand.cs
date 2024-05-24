using Application.Common.Exceptions;
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

namespace Application.TodoItems.Commands.Create
{
    public record CreateTodoItemCommand : IRequest<IResult>
    {
        public CreateTodoItemCommand()
        {
            Tags = new List<string>();
        }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string BackgroundColor { get; set; }
        public List<string> Tags { get; set; }
        public int ListId { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, IResult>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CreateTodoItemCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IResult> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var existList = await _applicationDbContext.TodoLists.FirstOrDefaultAsync(x => x.Id == request.ListId);

            if (existList is null)
            {
                return new ErrorResult("List not found.");
            }
            
            var newTodoItem = new TodoItem();
            newTodoItem.Title = request.Title;
            newTodoItem.ListId = request.ListId;
            newTodoItem.List = existList;
            newTodoItem.BackgroundColor = request.BackgroundColor;
            _applicationDbContext.TodoItems.Add(newTodoItem);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            
            return new SuccessResult();
        }
    }
}
