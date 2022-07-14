namespace HR_Evaluate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sumary
    {
        public int ID { get; set; }

        public int? BodID { get; set; }
        public virtual BOD BOD { get; set; }

        public int? EmpID { get; set; }
        public virtual Employee Employee { get; set; }

        public int? QuestionID { get; set; }
        public virtual Question Question { get; set; }

        [StringLength(500)]
        public string AnswerName { get; set; }

        public int? Score { get; set; }

        public int? TotalScore { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? Evaluatetimes { get; set; }
        public int? CurrentLevelID { get; set; }
        public virtual Level Level { get; set; }

        public int? CurrentPositionId { get; set; }
        public virtual Position Position { get; set; }

        public int? NextLevelID { get; set; }
        public int? NextPositionId { get; set; }
        public int? EvaluatePeriod { get; set; }
        public int? EvaluateYearId { get; set; }
    }
}
