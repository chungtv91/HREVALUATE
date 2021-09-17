using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRE.Core.Entities
{
    public class Level
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "LevelName is not Empty")]
        [StringLength(10)]
        public string LevelName { get; set; }

        public virtual List<BOD> BODs { get; set; } = new();

        public virtual List<Employee> Employees { get; set; } = new();

        public virtual List<Question> Questions { get; set; } = new();
    }
}
