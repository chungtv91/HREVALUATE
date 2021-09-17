using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRE.Core.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(350)]
        public string Name { get; set; }

        [StringLength(350)]
        public string? Img { get; set; }

        public int? DepartmentId { get; set; }

        public int? CurrentPositionId { get; set; }

        public int? CurrentLevelId { get; set; }

        public DateTime? DateEvaluate { get; set; }

        [StringLength(350)]
        public string? Targets { get; set; }

        public int? NextPositionId { get; set; }

        public int? NextLevelId { get; set; }

        public int? IsEnable { get; set; }

        public virtual Department Department { get; set; }

        public virtual Position Position { get; set; }

        public virtual Level Level { get; set; }

        public virtual List<ManagerEmp> ManagerEmps { get; set; } = new();

        public virtual List<Sumary> Sumaries { get; set; } = new();
    }
}
