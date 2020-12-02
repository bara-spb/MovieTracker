using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using bMovieTracker.Domain;

namespace bMovieTracker.App
{
    public interface IMoviesRepository: IRepository<Movie>
    {
        IQueryable<Movie> GetAllForUserAsQueryable(int userId);
        Task<IEnumerable<Movie>> GetAllForUser(int userId, CancellationToken ct = default(CancellationToken));

    }
}
