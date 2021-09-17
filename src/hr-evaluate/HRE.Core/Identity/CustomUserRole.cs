using Microsoft.AspNetCore.Identity;

namespace HRE.Core.Identity
{
    public class CustomUserRole : IdentityUserRole<int>
    {
        public virtual CustomUser User { get; set; }

        public virtual CustomRole Role { get; set; }
    }
}
