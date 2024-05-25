using Core.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TodoItemTag : BaseAuditableEntity
    {
        public int TodoItemId { get; set; }
        public string Tag { get; set; }
    }
}
