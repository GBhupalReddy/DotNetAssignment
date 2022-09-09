using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V2
{
    [ApiVersion("1.0")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserController : ApiControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, ILogger<UserController> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }


        // GET: <UserController>
        [ApiVersion("2.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            _logger.LogInformation("Getting list of all Users");
            var result = await _userRepository.GetUsersAsync();
            if (!result.Any())
                return NotFound();
            return Ok(result);
        }

        // GET <UserController>/5
        [ApiVersion("2.0")]
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
            _logger.LogInformation("Getting Id : {id} User", id);
            var user = await _userRepository.GetUserAsync(id);
            var result = _mapper.Map<User, UserDto>(user);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);

        }

        // POST <UserController>
        [ApiVersion("2.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] UserVm userVm)
        {
            _logger.LogInformation("add new user");
            var user = _mapper.Map<UserVm, User>(userVm);
            var userresult = await _userRepository.AddUserAsync(user);
            var result = _mapper.Map<User, UserDto>(userresult);
            return Ok(result);
        }

        // PUT <UserController>/5
        [ApiVersion("2.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] UserVm userVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's Id.");
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} User", id);
            var user = await _userRepository.UpdateUserAsynce(id, _mapper.Map<UserVm, User>(userVm));
            var result = _mapper.Map<User, UserDto>(user);
            if (result.Equals(null))
                return NoContent();
            return Ok(result);
        }

        // DELETE <UserController>/5
        [ApiVersion("2.0")]
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
            _logger.LogInformation("Deleted Id :  {id}  User", id);
            await _userRepository.DeleteUserAsync(id);
        }
    }
}
