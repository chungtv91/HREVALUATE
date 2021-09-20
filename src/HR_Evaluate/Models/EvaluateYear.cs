namespace HR_Evaluate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EvaluateYear")]
    public class EvaluateYear
    {
        public EvaluateYear()
        {
            ManagerEmps = new HashSet<ManagerEmp>();
        }

        public int Id { get; set; }

        public DateTime? FromYear { get; set; }

        public DateTime? ToYear { get; set; }

        public DateTime? NextEvaluatePeriod { get; set; }

        public virtual ICollection<ManagerEmp> ManagerEmps { get; set; }
    }
}
