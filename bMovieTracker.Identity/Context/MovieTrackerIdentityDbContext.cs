using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bMovieTracker.Identity
{
    public class MovieTrackerIdentityDbContext : IdentityDbContext<MovieTrackerUser, MovieTrackerRole, int>
    {
        public MovieTrackerIdentityDbContext(DbContextOptions<MovieTrackerIdentityDbContext> options) : base(options)
        {
        }
    }
}
