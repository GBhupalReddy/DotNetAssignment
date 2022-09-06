using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MovieController : ApiControllerBase
    {

        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MovieController> _logger;
        private readonly IMapper _mapper;
        public MovieController(IMovieRepository movieRepository, ILogger<MovieController> logger, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <MovieController>
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
        {
            _logger.LogInformation("Getting list of all Movies");
            var result = await _movieRepository.GetMoviesAsync();
            return Ok(result);
        }

        // GET <MovieController>/5
        [Route("{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting Id {id} Movie", id);
            var movie = await _movieRepository.GetMovieAsync(id);
            var result = _mapper.Map<Movie,MovieDto>(movie);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        // POST <MovieController>
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] MovieVm movieVm)
        {
           
            _logger.LogInformation("add new Movie");
            var movie=_mapper.Map<MovieVm,Movie>(movieVm);
            var movieResult = await _movieRepository.AddMovieAsync(movie);
            var result = _mapper.Map<Movie, MovieDto>(movieResult);
            return Ok(result);
        }

        // PUT <MovieController>/5
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] MovieVm movieVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {id}",id);
                return BadRequest();
            }
            _logger.LogInformation("Update Id: {id} Movie",id);
            var movie = _mapper.Map<MovieVm, Movie>(movieVm);
            var movieResult = await _movieRepository.UpdateMovieAsynce(id,movie);
            var result = _mapper.Map<Movie, MovieDto>(movieResult);
            return Ok(result);
        }

        // DELETE <MovieController>/5
        [Route("{id}")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
               
            }
            _logger.LogInformation("Deleted  {id}  Movie",id);
            await _movieRepository.DeleteMovieAsync(id);
        }
    }
}
