using AutoMapper;
using BookMyShowProject.Core.Contracts.infrastructure.Repository;
using BookMyShowProject.Core.Entities;
using BookMyShowProject.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShowProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<User>> Get(int id)
        {
            return await _userRepository.GetUsersAsync(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task Post([FromBody] UserVm userVm)
        {
            var user=_mapper.Map<UserVm,User>(userVm);
           await _userRepository.AddUserAsync(user);

        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
