using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HRE.Core.Identity
{
    public class CustomUserManager : UserManager<CustomUser>
    {
        public CustomUserManager(IUserStore<CustomUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<CustomUser> passwordHasher, IEnumerable<IUserValidator<CustomUser>> userValidators, IEnumerable<IPasswordValidator<CustomUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<CustomUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
