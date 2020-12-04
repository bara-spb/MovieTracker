using System;
using System.Threading.Tasks;
using bMovieTracker.App;
using bMovieTracker.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bMovieTracker.Api.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieTrackerService _movieService;
        private readonly MovieTrackerUserManager _userManager;

        public MoviesController(IMovieTrackerService movieService, MovieTrackerUserManager userManager)
        {
            _movieService = movieService;
            _userManager = userManager;
        }


        /// <summary>
        /// Get all movies
        /// </summary>
        /// <remarks>
        /// Returnes all movies for current user
        /// </remarks>
        /// <response code="200">Movies array in JSON format</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userId = _userManager.GetUserIdInt(User);
                if(userId == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "User not defined");
                var movies = await _movieService.GetAllMovies((int) userId);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get movie by id
        /// </summary>
        /// <remarks>
        /// Returnes particular movie
        /// </remarks>
        /// <response code="200">Movie in JSON format</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var userId = _userManager.GetUserIdInt(User);
                if (userId == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "User not defined");
                var movie = await _movieService.GetMovieById((int) userId, id);
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Create movie
        /// </summary>
        /// <remarks>
        /// Creates new movie
        /// </remarks>
        /// <response code="201">Created movie in JSON format</response>
        /// <response code="400">Model can't be empty</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateMovieVM movieVM)
        {
            try
            {
                if (movieVM == null)
                    return BadRequest("Model can't be empty");

                var userId = _userManager.GetUserIdInt(User);
                if (userId == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "User not defined");

                var newMovie = await _movieService.CreateMovie((int)userId, movieVM);
                if (newMovie != null)
                    return StatusCode(StatusCodes.Status201Created, newMovie);

                return StatusCode(StatusCodes.Status500InternalServerError, "Didn't manage to create the movie");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update movie
        /// </summary>
        /// <response code="200">Updated movie in JSON format</response>
        /// <response code="400">Model can't be empty</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MovieVM movieVM)
        {
            try
            {
                if (movieVM == null)
                    return BadRequest("Model can't be empty");

                var userId = _userManager.GetUserIdInt(User);
                if (userId == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "User not defined");

                if (await _movieService.GetMovieById((int) userId, id) == null)
                    return NotFound();

                movieVM.Id = id;
                if (await _movieService.UpdateMovie((int) userId, movieVM))
                    return Ok(movieVM);

                return StatusCode(StatusCodes.Status500InternalServerError, "Didn't manage to update the movie");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete movie
        /// </summary>
        /// <response code="200">Movie deleted</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Movie is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = _userManager.GetUserIdInt(User);
                if (userId == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "User not defined");

                if (await _movieService.GetMovieById((int) userId, id) == null)
                    return NotFound("Movie is not found");

                if (await _movieService.DeleteMovie((int) userId, id))
                    return Ok("Movie deleted");

                return StatusCode(StatusCodes.Status500InternalServerError, "Didn't manage to delete the movie");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
