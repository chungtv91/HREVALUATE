using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HRE.Core.Identity;

namespace HRE.Core.Entities
{
    /// <summary>
    /// Giám đốc
    /// </summary>
    public class BOD
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is not Empty")]
        [StringLength(10)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is not Empty")]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(350)]
        public string? Img { get; set; }

        public int? DepartmentID { get; set; }

        public int? PositionID { get; set; }

        public int? LevelID { get; set; }

        public virtual Department Department { get; set; }

        public virtual Level Level { get; set; }

        public virtual Position Position { get; set; }

        public virtual List<CustomUser> Logins { get; set; } = new();

        public virtual List<ManagerEmp> ManagerEmps { get; set; } = new();

        public virtual List<Sumary> Sumaries { get; set; } = new();
    }
}
