using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookinDetailsController : ControllerBase
    {

        private readonly IUserBookingDetailsService _userBookingDetailsService;
        private readonly IMapper _mapper;
        public UserBookinDetailsController(IUserBookingDetailsService userBookingDetailsService, IMapper mapper)
        {
            _userBookingDetailsService = userBookingDetailsService;
            _mapper = mapper;
        }
        // GET api/<UserBookinDetailsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
           var result = await _userBookingDetailsService.GetUserBookingDetalisAsync(id);
            if (!result.Any())
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

    }
}
