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

            
        }
    }
}
