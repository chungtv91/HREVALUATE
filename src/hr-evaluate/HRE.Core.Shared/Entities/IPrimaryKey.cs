using System;
using System.ComponentModel.DataAnnotations;

namespace HRE.Core.Shared.Entities
{
    public interface IPrimaryKey : IPrimaryKey<Guid>
    {
    }

    public interface IPrimaryKey<TKey>
    {
        [Key]
        TKey Id { get; set; }
    }
}
