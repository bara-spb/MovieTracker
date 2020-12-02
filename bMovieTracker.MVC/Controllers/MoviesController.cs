using Microsoft.AspNetCore.Mvc;
using bMovieTracker.App;
using System.Threading.Tasks;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using bMovieTracker.Identity;

namespace bMovieTracker.MVC.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieTrackerService _movieService;
        private readonly IMapper _mapper;
        private readonly MovieTrackerUserManager _userManager;
        public MoviesController(IMovieTrackerService movieService, IMapper mapper, MovieTrackerUserManager userManager)
        {
            _movieService = movieService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var movies = await _movieService.GetAllMovies(userId);
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
                    var userId = _userManager.GetUserId(User);
                    await _movieService.CreateMovie(userId, movieVM);
                    return RedirectToAction("Index");
                }
                else
                    return View("Error", "Didn't manage to create the movie");
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
                if (movieVM != null)
                {
                    var userId = _userManager.GetUserId(User);
                    if (await _movieService.UpdateMovie(userId, movieVM))
                        return RedirectToAction("Index");
                }
                return View("Error", "Didn't manage to update the movie");
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
                var userId = _userManager.GetUserId(User);
                if (await _movieService.DeleteMovie(userId, id))
                    return RedirectToAction("Index");
                else
                    return View("Error", "Didn't manage to delete the movie");
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }

        }

    }
}
