using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V2
{
    [ApiVersion("2.0")]
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


        

        [ApiVersion("2.0")]
        [Route("{cityName}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Getmovies(string cityName, string? language = null, string? genre = null, string? movieName=null)
        {
            _logger.LogInformation($"Get list of{cityName} {language}{genre} Movies ");
            var result = await _movieService.GetMovieLanguageGenreAsync(cityName, language, genre, movieName);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

      
        
    }
}
