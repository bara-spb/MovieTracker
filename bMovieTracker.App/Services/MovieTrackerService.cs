using bMovieTracker.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace bMovieTracker.App
{
    public class MovieTrackerService : IMovieTrackerService
    {
        private readonly IMoviesRepository _movieRep;
        private readonly IMapper _mapper;

        public MovieTrackerService(IMoviesRepository movieRep, IMapper mapper)
        {
            _movieRep = movieRep;
            _mapper = mapper;
        }


        public async Task<MovieVM> CreateMovie(int userId, CreateMovieVM createMovieVM, CancellationToken ct = default(CancellationToken))
        {
            var movie = _mapper.Map<Movie>(createMovieVM);
            movie.UserId = userId;
            movie = await _movieRep.Add(movie, ct);
            return _mapper.Map<MovieVM>(movie);
        }

        public async Task<IEnumerable<MovieVM>> GetAllMovies(int userId, CancellationToken ct = default(CancellationToken))
        {
            var movies = await _movieRep.GetAllForUser(userId, ct);
            return _mapper.Map<List<MovieVM>>(movies);
        }

        public async Task<MovieVM> GetMovieById(int userId, int id, CancellationToken ct = default(CancellationToken))
        {
            if (await IsOwner(userId, id))
            {
                var movie = await _movieRep.GetById(id, ct);
                return _mapper.Map<MovieVM>(movie);
            }
            throw new BMovieTrackerAccessDeniedException("Can't get access for another's movie");
        }

        public async Task<bool> UpdateMovie(int userId, MovieVM movieVM, CancellationToken ct = default(CancellationToken))
        {
            if (await IsOwner(userId, movieVM.Id))
            {
                var movie = await _movieRep.GetById(movieVM.Id, ct);
                if (movie == null) return false;
                movie.Title = movieVM.Title;
                movie.Genre = movieVM.Genre;
                movie.Rate = movieVM.Rate;
                movie.Year = movieVM.Year;
                return await _movieRep.Update(movie, ct);
            }
            throw new BMovieTrackerAccessDeniedException("Can't update another's movie");
        }

        public async Task<bool> DeleteMovie(int userId, int id, CancellationToken ct = default(CancellationToken))
        {
            if (await IsOwner(userId, id))
            {
                return await _movieRep.Delete(id, ct);
            }
            throw new BMovieTrackerAccessDeniedException("Can't delete another's movie");
        }

        private async Task<bool> IsOwner(int userId, int movieId)
        {
            var movie = await _movieRep.GetById(movieId);
            return movie?.UserId == userId;
        }
    }
}
