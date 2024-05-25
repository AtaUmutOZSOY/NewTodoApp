using Core.Enums;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public string? Note { get; set; }
    public PriorityEnum Priority { get; set; }
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

        var newTodoItem = new TodoItem()
        {
            Title = request.Title,
            Note = request.Note,
            IsCompleted = request.IsCompleted,
            BackgroundColor = request.BackgroundColor,
            ListId = request.ListId,
            List = existList,
            Priority = request.Priority,
            Tags = new List<TodoItemTag>() 
        };

        foreach (var tag in request.Tags)
        {
            if (tag == null) continue; 
            newTodoItem.Tags.Add(new TodoItemTag { Tag = tag });
        }

        _applicationDbContext.TodoItems.Add(newTodoItem);

        try
        {
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // Log the exception (You can replace this with your logging framework)
            Console.WriteLine($"Error saving new TodoItem: {ex.Message}");
            return new ErrorResult("An error occurred while saving the TodoItem.");
        }

        return new SuccessResult();
    }
}


