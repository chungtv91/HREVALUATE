using System.ComponentModel.DataAnnotations;
using HRE.Core.Identity;
using HRE.Core.Mappers;

namespace HRE.Web.UI.Areas.Admin.Models
{
    public class EditRoleViewModel : MapFrom<CustomRole>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
