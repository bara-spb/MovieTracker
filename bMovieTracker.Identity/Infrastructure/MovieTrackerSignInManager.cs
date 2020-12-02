using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace bMovieTracker.Identity
{
    public class MovieTrackerSignInManager : SignInManager<MovieTrackerUser>
    {

        public MovieTrackerSignInManager(
            MovieTrackerUserManager userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<MovieTrackerUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<MovieTrackerUser>> logger,
            IAuthenticationSchemeProvider schemes)
                : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }
    }
}
