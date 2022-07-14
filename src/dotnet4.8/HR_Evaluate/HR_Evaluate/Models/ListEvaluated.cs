using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace HR_Evaluate.Models
{
    [Table("ListEvaluated")]
    public class ListEvaluated
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }

        [StringLength(350)]
        public string Name { get; set; }

        [StringLength(350)]
        public string Img { get; set; }

        [StringLength(100)]
        public string DepartmentName { get; set; }

        [StringLength(100)]
        public string PositionName { get; set; }

        [StringLength(10)]
        public string LevelName { get; set; }

    }
}