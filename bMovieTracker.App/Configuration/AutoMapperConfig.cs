using AutoMapper;
using bMovieTracker.Domain;

namespace bMovieTracker.App.Configuration
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<Movie, MovieVM>();
            CreateMap<MovieVM, Movie>();
            CreateMap<Movie, CreateMovieVM>();
            CreateMap<CreateMovieVM, Movie>();
        }
    }
}
