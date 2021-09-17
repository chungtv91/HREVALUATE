using System.ComponentModel.DataAnnotations;

namespace HRE.Web.UI.Areas.Admin.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
