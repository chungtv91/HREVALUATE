using System;
using System.ComponentModel.DataAnnotations;
using HRE.Core.Shared.Auditing;
using HRE.Core.Shared.Entities;

namespace HRE.Core.Entities
{
    public abstract class Entity : Entity<Guid>, IPrimaryKey
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }

    public abstract class Entity<TKey> : FullAuditedEntity, IPrimaryKey<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
