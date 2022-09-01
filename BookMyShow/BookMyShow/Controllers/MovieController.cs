using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public MovieController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        // GET: api/<MovieController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _movieRepository.GetMoviesAsync();
            return Ok(result);
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _movieRepository.GetMovieAsync(id);
            return Ok(result);
        }

        // POST api/<MovieController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MovieVm movieVm)
        {
            var movie=_mapper.Map<MovieVm,Movie>(movieVm);
            var result=await _movieRepository.AddMovieAsync(movie);
            return Ok(result);
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] MovieVm movieVm)
        {
            var movie = _mapper.Map<MovieVm, Movie>(movieVm);
            var result = await _movieRepository.UpdateMovieAsynce(id,movie);
            return Ok(result);
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
             await _movieRepository.DeleteMovieAsync(id);
        }
    }
}
