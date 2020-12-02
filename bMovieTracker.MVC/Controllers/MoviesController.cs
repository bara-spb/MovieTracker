using Microsoft.AspNetCore.Mvc;
using bMovieTracker.App;
using System.Threading.Tasks;
using System;
using AutoMapper;

namespace bMovieTracker.MVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieTrackerService _movieService;
        private readonly IMapper _mapper;
        public MoviesController(IMovieTrackerService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var movies = await _movieService.GetAllMovies();
                return View("MoviesList", movies);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public IActionResult StartCreating()
        {
            return View("Create");
        }

        public async Task<IActionResult> Create(CreateMovieVM movieVM)
        {
            try
            {
                if (movieVM != null)
                {
                    await _movieService.CreateMovie(movieVM);
                    return RedirectToAction("Index");
                }
                else
                    return View("Error", "Home");
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public IActionResult StartEditing(MovieVM movieVM)
        {
            return View("Edit", movieVM);
        }

        public async Task<IActionResult> Edit(MovieVM movieVM)
        {
            try
            {
                if (movieVM != null) {
                    if (await _movieService.UpdateMovie(movieVM))
                        return RedirectToAction("Index");
                }
                return View("Error", "Didn't mange to update the movie");
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _movieService.DeleteMovie(id))
                    return RedirectToAction("Index");
                else
                    return View("Error", "Didn't mange to delete the movie");
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }

        }

    }
}
