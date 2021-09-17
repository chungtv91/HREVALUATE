using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HRE.Core.Identity;
using HRE.Core.Mappers;
using HRE.Core.Shared.Mappers;

namespace HRE.Web.UI.Areas.Admin.Models
{
    public class EditUserViewModel : IMapFrom<CustomUser>
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [NoMap]
        // [Required]
        public string Password { get; set; }

        public bool IsDisabled { get; set; }

        public int RoleID { get; set; }

        public int? BODID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomUser, EditUserViewModel>()
                .ForMember(d => d.RoleID, s => s.MapFrom(x => x.Roles.Select(r => r.Id).FirstOrDefault()))
                .IgnoreNoMap();
        }
    }
}
