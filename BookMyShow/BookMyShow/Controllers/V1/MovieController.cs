using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("movie")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MovieController : ApiControllerBase
    {

        private readonly IMovieService _movieService;
        private readonly IExceptionService _exceptionService;
        private readonly ILogger<MovieController> _logger;
        private readonly IMapper _mapper;
        public MovieController(IMovieService movieService, IExceptionService exceptionService, ILogger<MovieController> logger, IMapper mapper)
        {
            _movieService = movieService;
            _exceptionService = exceptionService;
            _logger = logger;
            _mapper = mapper;
        }


        // GET: <MovieController>
        [ApiVersion("1.0")]
        [Route(""), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            _logger.LogInformation("Get list of all Movies");
            var result = await _movieService.GetMoviesAsync();
            return Ok(result);
        }

        // GET <MovieController>/5
        [ApiVersion("1.0")]
        [Route("{id}"), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<MovieDto>> GetMovieById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Get Id {id} Movie", id);
            var movie = await _movieService.GetMovieByIdAsync(id);
            var result = _mapper.Map<Movie, MovieDto>(movie);
            if (result is null)
                await _exceptionService.VerifyIdExist(id, "Movie");
            return Ok(result);
        }

        // POST <MovieController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<MovieDto>> PostMovie([FromBody] MovieVm movieVm)
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
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<MovieDto>> PutMovie(int id, [FromBody] MovieVm movieVm)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Update Id: {id} Movie", id);
            var movie = _mapper.Map<MovieVm, Movie>(movieVm);
            var movieResult = await _movieService.UpdateMovieAsynce(id, movie);
            var result = _mapper.Map<Movie, MovieDto>(movieResult);
            if (result is null)
                await _exceptionService.VerifyIdExist(id, "Movie");
            return Ok(result);
        }

        // DELETE <MovieController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task DeleteMovie(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Deleted  {id}  Movie", id);
            await _movieService.DeleteMovieAsync(id);
        }
        // GET<MovieController> Movie/City/language/genre

        [ApiVersion("1.0")]
        [Route("movies-{cityName}"), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<MovieDetailes>>> GetCityMovies(string cityName, string movieName, string? date = null)
        {
            _logger.LogInformation($"Get list of{cityName} {date} {movieName} ");
            var result = await _movieService.GetMovieLanguageGenreAsync(cityName, movieName, date);
            if (!result.Any())
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        [ApiVersion("1.0")]
        [Route("seatstatus-{showId}"), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<SeatStatus>>> GetSeatStatus(int showId)
        {
            var result = await _movieService.GetSeatstatus(showId);
            return Ok(result);
        }
    }
}
