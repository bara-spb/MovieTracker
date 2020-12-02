using bMovieTracker.App;
using Microsoft.AspNetCore.Identity;

namespace bMovieTracker.Identity
{
    public class MovieTrackerUser : IdentityUser<int>, IMovieTrackerUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
