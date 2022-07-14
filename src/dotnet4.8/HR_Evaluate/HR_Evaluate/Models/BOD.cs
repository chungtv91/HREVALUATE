namespace HR_Evaluate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BOD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOD()
        {
            Logins = new HashSet<Login>();
            ManagerEmps = new HashSet<ManagerEmp>();
            Sumaries = new HashSet<Sumary>();
        }

        public int Id { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Code is not Empty")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is not Empty")]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(350)]
        public string Img { get; set; }

        public int? DepartmentID { get; set; }

        public int? PositionID { get; set; }

        public int? LevelID { get; set; }

        public virtual Department Department { get; set; }

        public virtual Level Level { get; set; }

        public virtual Position Position { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Login> Logins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManagerEmp> ManagerEmps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sumary> Sumaries { get; set; }
    }
}
