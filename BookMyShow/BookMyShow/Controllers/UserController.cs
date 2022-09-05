using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
  
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


        /// </summary>
        /// <returns></returns>
        // GET: api/<UserController>
        [HttpGet]
        public async  Task<ActionResult<IEnumerable<User>>> Get()
        {
            _logger.LogInformation("Getting list of all Users");
            var result = await _userRepository.GetUsersAsync();
            if (!result.Any())
                return NotFound();
            return Ok(result);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting Id : {id} User", id);
            return Ok( await _userRepository.GetUserAsync(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserVm userVm)
        {
            _logger.LogInformation("add new user");
            var user = _mapper.Map<UserVm, User>(userVm);
            var ok = await _userRepository.AddUserAsync(user);
            var result = _mapper.Map<User, UserDto>(ok);
            return Ok(result);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserVm userVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's Id.");
                return BadRequest();
            }
            _logger.LogInformation("Update Id: {id} User", id);
            var ok = await _userRepository.UpdateUserAsynce(id, _mapper.Map<UserVm, User>(userVm));
            var result = _mapper.Map<User,UserDto>(ok);
            return Ok(result);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                
            }
            _logger.LogInformation("Deleted Id :  {id}  User", id);
            await _userRepository.DeleteUserAsync(id);
        }
    }
}
