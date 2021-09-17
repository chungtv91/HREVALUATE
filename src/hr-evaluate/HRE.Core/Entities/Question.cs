using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRE.Core.Entities
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1500)]
        public string QuestionName { get; set; }

        public int? SortOrder { get; set; }

        public int? LevelId { get; set; }
        public virtual Level Level { get; set; }

        public virtual List<Sumary> Sumaries { get; set; } = new();
    }
}
