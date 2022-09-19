using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MovieController : ApiControllerBase
    {

        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger, IMapper mapper)
        {
            _movieService = movieService;
            _logger = logger;
            _mapper = mapper;
        }


        // GET: <MovieController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
        {
            _logger.LogInformation("Get list of all Movies");
            var result = await _movieService.GetMoviesAsync();
            return Ok(result);
        }

        // GET <MovieController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Get Id {id} Movie", id);
            var movie = await _movieService.GetMovieByIdAsync(id);
            var result = _mapper.Map<Movie, MovieDto>(movie);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }
   

        // GET<MovieController>Movie/City/language/genre
        [ApiVersion("1.0")]
       // [Route("{cityName}")]
        [HttpGet("cityName")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Getmovies(string cityName, string? language=null, string? genre=null, string? movieName = null)
        {
            _logger.LogInformation($"Get list of{cityName} {language}{genre} Movies ");
            var result = await _movieService.GetMovieLanguageGenreAsync(cityName, language, genre, movieName);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        // POST <MovieController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] MovieVm movieVm)
        {

            _logger.LogInformation("add new Movie");
            var movie = _mapper.Map<MovieVm, Movie>(movieVm);
            var movieResult = await _movieService.AddMovieAsync(movie);
            var result = _mapper.Map<Movie, MovieDto>(movieResult);
            return Ok(result);
        }

        // PUT <MovieController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] MovieVm movieVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} Movie", id);
            var movie = _mapper.Map<MovieVm, Movie>(movieVm);
            var movieResult = await _movieService.UpdateMovieAsynce(id, movie);
            var result = _mapper.Map<Movie, MovieDto>(movieResult);
            return Ok(result);
        }

        // DELETE <MovieController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Deleted  {id}  Movie", id);
            await _movieService.DeleteMovieAsync(id);
        }
    }
}
