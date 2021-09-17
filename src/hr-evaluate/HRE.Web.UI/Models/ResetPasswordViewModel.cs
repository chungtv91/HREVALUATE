using System.ComponentModel.DataAnnotations;

namespace HRE.Web.UI.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
