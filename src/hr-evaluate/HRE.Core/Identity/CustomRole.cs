using System;
using System.Collections.Generic;
using HRE.Core.Runtime.Validation;
using HRE.Core.Shared.Auditing;
using HRE.Core.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace HRE.Core.Identity
{
    public class CustomRole : IdentityRole<int>, IPrimaryKey<int>, IFullAudited, IShouldNormalize
    {
        public string? Description { get; set; }

        public int? CreatorUserId{ get; set; }
        public DateTime CreationTime{ get; set; }
        public int? LastModifierUserId{ get; set; }
        public DateTime? LastModificationTime{ get; set; }
        public bool IsDeleted{ get; set; }
        public int? DeleterUserId{ get; set; }
        public DateTime? DeletionTime{ get; set; }

        public virtual List<CustomUser> Users { get; set; } = new();

        public void Normalize()
        {
            
        }
    }
}
