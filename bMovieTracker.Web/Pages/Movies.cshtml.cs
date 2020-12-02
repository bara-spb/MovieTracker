using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bMovieTracker.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bMovieTracker.Web.Pages
{
    public class MoviesModel : PageModel
    {
        private readonly IMovieTrackerService _movieService;
        public MoviesModel(IMovieTrackerService movieService)
        {
            _movieService = movieService;
        }


        public async Task<IEnumerable<MovieVM>> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            return movies;
        }
    }
}