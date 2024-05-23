using Core.Entity;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TodoList:BaseAuditableEntity
    {
        public PriortyEnum PriorityLevel { get; set; }
        public string Title { get; set; }
        public ICollection<TodoItem> Items { get; set; } = new List<TodoItem>();

    }
}
