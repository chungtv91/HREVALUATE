using System;
using System.ComponentModel.DataAnnotations;

namespace HRE.Core.Entities
{
    /// <summary>
    /// Bản ghi nhớ đánh giá nhân viên hàng ngày của giám đốc
    /// </summary>
    public class BodMemo
    {
        public int Id { get; set; }

        public int BodId { get; set; }

        public virtual BOD BOD { get; set; }

        public int EmpId { get; set; }

        public virtual Employee Employee { get; set; }

        [StringLength(1000)]
        public string? Pros { get; set; }

        [StringLength(1000)]
        public string? Cons { get; set; }

        [StringLength(1000)]
        public string? Improve { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? updatedate { get; set; }

        public int? MonthOfMemo { get; set; }

        //public int? EvaluateYearId { get; set; }

        //public virtual EvaluateYear EvaluateYear { get; set; }
    }
}