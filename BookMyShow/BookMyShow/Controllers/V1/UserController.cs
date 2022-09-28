using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("user")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserController : ApiControllerBase
    {

        private readonly IUserService _userService;
        private readonly IExceptionService _exceptionService;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IExceptionService exceptionService, ILogger<UserController> logger, IMapper mapper)
        {
            _userService = userService;
            _exceptionService = exceptionService;
            _logger = logger;
            _mapper = mapper;
        }


        // GET: 
        [ApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            _logger.LogInformation("Getting list of all Users");
            var result = await _userService.GetUsersAsync();
            return Ok(result);
        }

        // GET 
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]

        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Getting Id : {id} User", id);
            var user = await _userService.GetUserByIdAsync(id);
            var result = _mapper.Map<User, UserDto>(user);
            if (result is null)
                await _exceptionService.VerifyIdExist(id, "User");
            return Ok(result);

        }

        // POST 
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] UserVm userVm)
        {
            await _userService.UserExitByEmail(userVm.Email);

            _logger.LogInformation("add new user");
            var user = _mapper.Map<UserVm, User>(userVm);
            var userresult = await _userService.AddUserAsync(user);
            var result = _mapper.Map<User, UserDto>(userresult);
            return Ok(result);
        }

        // PUT 
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] UserVm userVm)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's Id.");
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Update Id: {id} User", id);
            var user = await _userService.UpdateUserAsynce(id, _mapper.Map<UserVm, User>(userVm));
            var result = _mapper.Map<User, UserDto>(user);
            if (result.Equals(null))
                await _exceptionService.VerifyIdExist(id, "User");
            return Ok(result);
        }

        // DELETE <UserController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Deleted Id :  {id}  User", id);
            await _userService.DeleteUserAsync(id);
        }

        // GET UserBokingDetails

        [ApiVersion("1.0")]
        [Route("userbokingdetails/{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]

        public async Task<ActionResult> GetBookingDeatils(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Getting Id : {id} User", id);
            var result = await _userService.GetUserBookingDetalisAsync(id);
            if (!result.Any())
            {
                await _exceptionService.VerifyIdExist(id, "User");
            }
            return Ok(result);

        }
    }
}
