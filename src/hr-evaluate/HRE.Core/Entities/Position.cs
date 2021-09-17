using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRE.Core.Entities
{
    public class Position
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "PositionName is not Empty")]
        [StringLength(100)]
        public string PositionName { get; set; }

        public virtual List<BOD> BODs { get; set; } = new();

        public virtual List<Employee> Employees { get; set; } = new();
    }
}
