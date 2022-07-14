namespace HR_Evaluate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ManagerEmp
    {
        public int Id { get; set; }

        public int? EmployeeID { get; set; }

        public int? BODID { get; set; }

        public int? EvaluateYearId { get; set; }

        public DateTime? FirstPeriodEvaluate { get; set; }
        public DateTime? SecondPeriodEvaluate { get; set; }
        public DateTime? ThirdPeriodEvaluate { get; set; }

        public virtual BOD BOD { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EvaluateYear EvaluateYear { get; set; }
    }
}
