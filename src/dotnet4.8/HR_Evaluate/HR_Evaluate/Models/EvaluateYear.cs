namespace HR_Evaluate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EvaluateYear")]
    public partial class EvaluateYear
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EvaluateYear()
        {
            BodMemoes = new HashSet<BodMemo>();
            ManagerEmps = new HashSet<ManagerEmp>();
        }

        public int Id { get; set; }

        public DateTime? FromYear { get; set; }

        public DateTime? ToYear { get; set; }

        public DateTime? NextEvaluatePeriod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BodMemo> BodMemoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManagerEmp> ManagerEmps { get; set; }
    }
}
