using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HRE.Core.Entities;
using HRE.Core.Runtime.Validation;
using HRE.Core.Shared.Auditing;
using HRE.Core.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace HRE.Core.Identity
{
    public class CustomUser : IdentityUser<int>, IPrimaryKey<int>, IFullAudited, IShouldNormalize
    {
        [Required]
        [StringLength(120)] 
        public string FullName { get; set; }

        public int? BODID { get; set; }
        public virtual BOD? BOD { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }

        public virtual List<CustomRole> Roles { get; set; } = new();

        public void Normalize()
        {

        }
    }
}
