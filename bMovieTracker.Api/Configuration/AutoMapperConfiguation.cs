using AutoMapper;
using bMovieTracker.Api.Models;
using bMovieTracker.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bMovieTracker.Api.Configuration
{

    public class MovieViewModelMappingProfile : Profile
    {
        public MovieViewModelMappingProfile()
        {
            CreateMap<MovieViewModel, MovieVM>();
            CreateMap<MovieVM, MovieViewModel>();
        }
    }
}
