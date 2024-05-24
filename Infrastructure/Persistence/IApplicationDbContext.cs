using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Infrastructure.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }
        DbSet<TodoItem> TodoItems { get; set; }
        DbSet<TodoItemTag> TodoItemTags { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
