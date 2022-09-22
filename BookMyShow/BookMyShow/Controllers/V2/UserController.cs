using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V2
{
    [ApiVersion("1.0")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserController : ApiControllerBase
    {

        private readonly IUserService _UserService;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper)
        {
            _UserService = userService;
            _logger = logger;
            _mapper = mapper;
        }


      

        // GET <UserController>/5
        [ApiVersion("2.0")]
        [Route("UaerBokingDetails/{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]

        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Getting Id : {id} User", id);
            var result = await _UserService.GetUserBookingDetalisAsync(id);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);

        }

            
        }
}
