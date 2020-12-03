using System;
using System.Threading.Tasks;
using AutoMapper;
using bMovieTracker.App;
using bMovieTracker.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace bMovieTracker.Api.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieTrackerService _movieService;
        private readonly IMapper _mapper;
        private readonly MovieTrackerUserManager _userManager;

        public MoviesController(IMovieTrackerService movieService, IMapper mapper, MovieTrackerUserManager userManager)
        {
            _movieService = movieService;
            _mapper = mapper;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if(userId == null)
                    return StatusCode(500, "User not defined");
                var movies = await _movieService.GetAllMovies((int) userId);
                var result = JsonConvert.SerializeObject(movies);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                    return StatusCode(500, "User not defined");
                var movie = await _movieService.GetMovieById((int) userId, id);
                var result = JsonConvert.SerializeObject(movie);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateMovieVM movieVM)
        {
            try
            {
                if (movieVM == null)
                    return BadRequest();

                var userId = _userManager.GetUserId(User);
                if (userId == null)
                    return StatusCode(500, "User not defined");

                var newMovie = await _movieService.CreateMovie((int)userId, movieVM);
                if (newMovie != null)
                    return Ok(newMovie);

                return StatusCode(500, "Didn't manage to create the movie");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MovieVM movieVM)
        {
            try
            {
                if (movieVM == null)
                    return BadRequest();

                var userId = _userManager.GetUserId(User);
                if (userId == null)
                    return StatusCode(500, "User not defined");

                if (await _movieService.GetMovieById((int) userId, id) == null)
                    return NotFound();

                movieVM.Id = id;
                if (await _movieService.UpdateMovie((int) userId, movieVM))
                    return Ok(movieVM);

                return StatusCode(500, "Didn't manage to update the movie");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                    return StatusCode(500, "User not defined");

                if (await _movieService.GetMovieById((int) userId, id) == null)
                    return NotFound();

                if (await _movieService.DeleteMovie((int) userId, id))
                    return Ok("Movie deleted");

                return StatusCode(500, "Didn't manage to delete the movie");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
