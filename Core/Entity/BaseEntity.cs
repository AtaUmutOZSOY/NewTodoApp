using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Status = EntityStatus.Active;
        }
        public int Id { get; set; }

        public EntityStatus Status { get; set; }
    }
}

