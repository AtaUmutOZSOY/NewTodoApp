using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Core.Entity;
using System;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoItemTag> TodoItemTags { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "system";
                        entry.Entity.Created = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "system";
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItemTag>()
                .HasKey(t => t.Id);

            // Seed data
            modelBuilder.Entity<TodoList>().HasData(
                new TodoList
                {
                    Id = 1,
                    Title = "Personal",
                    PriorityLevel = Core.Enums.PriorityEnum.High,
                    Created = DateTime.UtcNow,
                    CreatedBy = "system"
                },
                new TodoList
                {
                    Id = 2,
                    Title = "Work",
                    PriorityLevel = Core.Enums.PriorityEnum.Medium,
                    Created = DateTime.UtcNow,
                    CreatedBy = "system"
                }
            );

            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem
                {
                    Id = 1,
                    Title = "Buy groceries",
                    Note = "Milk, Bread, Eggs",
                    IsCompleted = false,
                    BackgroundColor = "#ffffff",
                    ListId = 1,
                    Priority = Core.Enums.PriorityEnum.Medium,
                    Created = DateTime.UtcNow,
                    CreatedBy = "system"
                },
                new TodoItem
                {
                    Id = 2,
                    Title = "Complete project report",
                    Note = "Due end of the week",
                    IsCompleted = false,
                    BackgroundColor = "#ffffff",
                    ListId = 2,
                    Priority = Core.Enums.PriorityEnum.High,
                    Created = DateTime.UtcNow,
                    CreatedBy = "system"
                }
            );

            modelBuilder.Entity<TodoItemTag>().HasData(
                new TodoItemTag
                {
                    Id = 1,
                    TodoItemId = 1,
                    Tag = "Shopping",
                    Created = DateTime.UtcNow,
                    CreatedBy = "system"
                },
                new TodoItemTag
                {
                    Id = 2,
                    TodoItemId = 2,
                    Tag = "Work",
                    Created = DateTime.UtcNow,
                    CreatedBy = "system"
                }
            );
        }
    }
}
