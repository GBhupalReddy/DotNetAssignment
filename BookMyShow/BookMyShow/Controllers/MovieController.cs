﻿using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    
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

        // GET: api/<MovieController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            _logger.LogInformation("Getting list of all Movies");
            var result = await _movieRepository.GetMoviesAsync();
            return Ok(result);
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting Id {id} Movie", id);
            var result = await _movieRepository.GetMovieAsync(id);
            return Ok(result);
        }

        // POST api/<MovieController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MovieVm movieVm)
        {
           
            _logger.LogInformation("add new Movie");
            var movie=_mapper.Map<MovieVm,Movie>(movieVm);
            var result=await _movieRepository.AddMovieAsync(movie);
            return Ok(result);
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] MovieVm movieVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {id}",id);
                return BadRequest();
            }
            _logger.LogInformation("Update Id: {id} Movie",id);
            var movie = _mapper.Map<MovieVm, Movie>(movieVm);
            var result = await _movieRepository.UpdateMovieAsynce(id,movie);
            return Ok(result);
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
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
