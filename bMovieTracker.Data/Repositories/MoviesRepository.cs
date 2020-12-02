using bMovieTracker.App;
using bMovieTracker.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace bMovieTracker.Data
{
    public class MoviesRepository : EFRepository<Movie>, IMoviesRepository
    {
        public MoviesRepository(MovieTrackerDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetAllForUser(int userId, CancellationToken ct = default(CancellationToken))
        {
            var movies = GetAllAsQueryable();
            return await movies.Where(m => m.UserId == userId).ToListAsync();
        }

        public IQueryable<Movie> GetAllForUserAsQueryable(int userId)
        {
            return GetAllAsQueryable().Where(m => m.UserId == userId);
        }
    }
}
