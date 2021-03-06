using System.ComponentModel.DataAnnotations;

namespace HRE.Web.UI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsRemember { get; set; }
    }
}
