using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CinemainCityController : ControllerBase
    {
        private readonly ICityNametoMovieNameService _movieRepository;
        private readonly IMapper _mapper;
        public CinemainCityController(ICityNametoMovieNameService movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;   
        }
       
        [HttpGet("{cityname}")]
        public async Task<ActionResult> Get(string cityname)
        {
          var result =await _movieRepository.GetCinemasAsync(cityname);
            if (!result.Any())
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

       
    }
}
