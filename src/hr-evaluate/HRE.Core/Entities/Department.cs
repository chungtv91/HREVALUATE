using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRE.Core.Entities
{
    /// <summary>
    /// Phòng ban
    /// </summary>
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "DepartmentName is not Empty")]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        public virtual List<BOD> BODs { get; set; } = new();

        public virtual List<Employee> Employees { get; set; } = new();
    }
}
