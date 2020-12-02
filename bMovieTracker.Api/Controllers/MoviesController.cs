using System;
using System.Threading.Tasks;
using AutoMapper;
using bMovieTracker.App;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace bMovieTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieTrackerService _movieService;
        private readonly IMapper _mapper;

        public MoviesController(IMovieTrackerService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var movies = await _movieService.GetAllMovies();
                var result = JsonConvert.SerializeObject(movies);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieById(id);
                var result = JsonConvert.SerializeObject(movie);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateMovieVM movieVM)
        {
            try
            {
                if (movieVM == null)
                    return BadRequest();

                return StatusCode(201, await _movieService.CreateMovie(movieVM));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MovieVM movieViewModel)
        {
            try
            {
                if (movieViewModel == null)
                    return BadRequest();

                if (await _movieService.GetMovieById(id) == null)
                    return NotFound();

                var movieVM = _mapper.Map<MovieVM>(movieViewModel);
                movieVM.Id = id;
                if (await _movieService.UpdateMovie(movieVM))
                    return Ok(movieViewModel);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _movieService.GetMovieById(id) == null)
                    return NotFound();

                if (await _movieService.DeleteMovie(id))
                    return Ok();

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
