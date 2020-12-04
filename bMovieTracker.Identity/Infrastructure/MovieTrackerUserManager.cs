using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace bMovieTracker.Identity
{
    public class MovieTrackerUserManager : UserManager<MovieTrackerUser>
    {

        public MovieTrackerUserManager(IUserStore<MovieTrackerUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<MovieTrackerUser> passwordHasher,
            IEnumerable<IUserValidator<MovieTrackerUser>> userValidators,
            IEnumerable<IPasswordValidator<MovieTrackerUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<MovieTrackerUserManager> logger
            ) :
                base(store,
                    optionsAccessor,
                    passwordHasher,
                    userValidators,
                    passwordValidators,
                    keyNormalizer,
                    errors,
                    services,
                    logger)
        {
        }

        public virtual int? GetUserIdInt(ClaimsPrincipal principal)
        {
            var idStr = base.GetUserId(principal);
            if (idStr != null && int.TryParse(idStr, out int id))
                return id;
            return null;
        }
    }
}
