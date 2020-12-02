using bMovieTracker.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace bMovieTracker.App
{
    public class MovieTrackerService : IMovieTrackerService
    {
        private readonly IRepository<Movie> _movieRep;
        private readonly IMapper _mapper;

        public MovieTrackerService(IRepository<Movie> movieRep, IMapper mapper)
        {
            _movieRep = movieRep;
            _mapper = mapper;
        }


        public async Task<MovieVM> CreateMovie(CreateMovieVM createMovieVM, CancellationToken ct = default(CancellationToken))
        {
            var movie = _mapper.Map<Movie>(createMovieVM);
            movie = await _movieRep.Add(movie, ct);
            return _mapper.Map<MovieVM>(movie);
        }

        public async Task<IEnumerable<MovieVM>> GetAllMovies(CancellationToken ct = default(CancellationToken))
        {
            var movies = await _movieRep.GetAll(ct);
            return _mapper.Map<List<MovieVM>>(movies);
        }

        public async Task<MovieVM> GetMovieById(int id, CancellationToken ct = default(CancellationToken))
        {
            var movie = await _movieRep.GetById(id, ct);
            return _mapper.Map<MovieVM>(movie);
        }

        public async Task<bool> UpdateMovie(MovieVM movieVM, CancellationToken ct = default(CancellationToken))
        {
            var movie = await _movieRep.GetById(movieVM.Id, ct);
            if (movie == null) return false;
            movie.Title = movieVM.Title;
            movie.Genre = movieVM.Genre;
            movie.Rate = movieVM.Rate;
            movie.Year = movieVM.Year;
            return await _movieRep.Update(movie, ct);
        }

        public async Task<bool> DeleteMovie(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _movieRep.Delete(id, ct);
        }
    }
}
