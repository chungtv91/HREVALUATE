namespace HR_Evaluate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            ManagerEmps = new HashSet<ManagerEmp>();
            Sumaries = new HashSet<Sumary>();
        }

        public int Id { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(350)]
        public string Name { get; set; }

        [StringLength(350)]
        public string Img { get; set; }

        public int? DepartmentId { get; set; }

        public int? CurrentPositionId { get; set; }

        public int? CurrentLevelId { get; set; }

        public DateTime? DateEvaluate { get; set; }

        [StringLength(350)]
        public string Targets { get; set; }

        public int? NextPositionId { get; set; }

        public int? NextLevelId { get; set; }

        public int? IsEnable { get; set; }

        public virtual Department Department { get; set; }

        public virtual Position Position { get; set; }

        public virtual Level Level { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManagerEmp> ManagerEmps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sumary> Sumaries { get; set; }
    }
}
