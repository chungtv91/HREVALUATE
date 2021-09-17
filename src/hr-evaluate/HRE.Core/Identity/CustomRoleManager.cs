using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HRE.Core.Identity
{
    public class CustomRoleManager: RoleManager<CustomRole>
    {
        public CustomRoleManager(IRoleStore<CustomRole> store, IEnumerable<IRoleValidator<CustomRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<CustomRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
