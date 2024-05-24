using Core.Entity;
using Core.Enums;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class TodoItem : BaseAuditableEntity
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string BackgroundColor { get; set; }
        public int ListId { get; set; }
        public TodoList List { get; set; }
        public PriortyEnum Priority { get; set; }

        public ICollection<TodoItemTag> Tags { get; set; }
    }
}
