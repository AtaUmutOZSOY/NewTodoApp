using Core.Entity;
using System;

namespace Domain.Entities
{
    public class TodoItemTag : BaseAuditableEntity
    {
        public int TodoItemId { get; set; }
        public TodoItem TodoItem { get; set; }
        public string Tag { get; set; }
    }
}
