using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace bMovieTracker.App
{
    public interface IMovieTrackerService
    {
        Task<MovieVM> CreateMovie(CreateMovieVM movieVM, CancellationToken ct = default(CancellationToken));
        Task<IEnumerable<MovieVM>> GetAllMovies(CancellationToken ct = default(CancellationToken));
        Task<MovieVM> GetMovieById(int id, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateMovie(MovieVM movie, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteMovie(int id, CancellationToken ct = default(CancellationToken));
    }
}
