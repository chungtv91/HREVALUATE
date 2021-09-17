using System.ComponentModel.DataAnnotations;

namespace HRE.Web.UI.Areas.Admin.Models
{
    public class CreateUserViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsDisabled { get; set; }

        public int RoleID { get; set; }

        public int? BODID { get; set; }
    }
}
