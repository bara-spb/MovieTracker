using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace bMovieTracker.App
{
    public interface IMovieTrackerService
    {
        Task<MovieVM> CreateMovie(int userId, CreateMovieVM movieVM, CancellationToken ct = default(CancellationToken));
        Task<IEnumerable<MovieVM>> GetAllMovies(int userId, CancellationToken ct = default(CancellationToken));
        Task<MovieVM> GetMovieById(int userId, int id, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateMovie(int userId, MovieVM movie, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteMovie(int userId, int id, CancellationToken ct = default(CancellationToken));
    }
}
