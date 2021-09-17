using System;
using System.Collections.Generic;

namespace HRE.Core.Entities
{
    /// <summary>
    /// Đánh giá năm.
    /// </summary>
    public class EvaluateYear
    {
        public int Id { get; set; }

        public DateTime? FromYear { get; set; }

        public DateTime? ToYear { get; set; }

        public DateTime? NextEvaluatePeriod { get; set; }

        //public virtual List<BodMemo> BodMemoes { get; set; } = new();

        public virtual List<ManagerEmp> ManagerEmps { get; set; } = new();
    }
}
