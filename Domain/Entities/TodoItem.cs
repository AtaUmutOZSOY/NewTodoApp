using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TodoItem : BaseAuditableEntity
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string BackgroundColor { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public int ListId { get; set; }
        public TodoList List
        {
            get; set;
        }
    }
}
