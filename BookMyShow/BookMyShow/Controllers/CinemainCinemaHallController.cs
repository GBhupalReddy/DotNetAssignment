using BookMyShow.Core.Contracts.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemainCinemaHallController : ControllerBase
    {
        private readonly ICinemaHallinCinemaService _cinemaHallinCinemaService;
        public CinemainCinemaHallController(ICinemaHallinCinemaService cinemaHallinCinemaService)
        {
            _cinemaHallinCinemaService = cinemaHallinCinemaService;
        }

        // GET api/<CinemainCinemaHallController>/5
        [HttpGet("{cinemaName}, {cityName}")]
        public async Task<ActionResult> Get(string cinemaName, string cityName)
        {
            var result = await _cinemaHallinCinemaService.GetCinemainCinemaHall(cinemaName, cityName);
            if (!result.Any())
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

       
    }
}
