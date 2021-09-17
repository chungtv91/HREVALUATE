using System.ComponentModel.DataAnnotations;

namespace HRE.Core.Entities
{
    /// <summary>
    /// View
    /// </summary>
    public class ListEvaluated
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        [StringLength(350)]
        public string Name { get; set; }

        [StringLength(350)]
        public string? Img { get; set; }

        [StringLength(100)]
        public string? DepartmentName { get; set; }

        [StringLength(100)]
        public string? PositionName { get; set; }

        [StringLength(10)]
        public string? LevelName { get; set; }

    }
}