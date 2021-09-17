using System;

namespace HRE.Core.Entities
{
    public class ManagerEmp
    {
        public int Id { get; set; }

        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public int BODID { get; set; }
        public virtual BOD BOD { get; set; }

        public int? EvaluateYearId { get; set; }
        public virtual EvaluateYear EvaluateYear { get; set; }

        public DateTime? FirstPeriodEvaluate { get; set; }
        public DateTime? SecondPeriodEvaluate { get; set; }
        public DateTime? ThirdPeriodEvaluate { get; set; }
    }
}
