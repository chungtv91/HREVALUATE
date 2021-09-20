namespace HR_Evaluate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BodMemo")]
    public class BodMemo
    {
        public int Id { get; set; }

        public int? BodId { get; set; }

        public int? EmpId { get; set; }

        [StringLength(1000)]
        public string Pros { get; set; }

        [StringLength(1000)]
        public string Cons { get; set; }

        [StringLength(1000)]
        public string Improve { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? updatedate { get; set; }

        public int? MonthOfMemo { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual BOD BOD { get; set; }

    }
}