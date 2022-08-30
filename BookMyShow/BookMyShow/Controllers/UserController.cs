using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using BookMyShow.Infrastructure.Repository.EntityFramWork;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly IUserRepository _userRepository;
        //public UserController()
        //{
        //    _userRepository = new UserRepository(new BookMyShowContext());
        //}
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        /// </summary>
        /// <returns></returns>
        // GET: api/<UserController>
        [HttpGet]
        public async  Task<IEnumerable<UserDto>> Get()
        {
            return await _userRepository.GetUsersAsync();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            //var result=await _userRepository.GetUserAsync(id);

            //var ok =  _mapper.Map<User, UserDto>(await _userRepository.GetUserAsync(id));
          return Ok( await _userRepository.GetUserAsync(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserVm userVm)
        {
            //var user = _mapper.Map<UserVm, User>(userVm);
            var ok = await _userRepository.AddUserAsync(_mapper.Map<UserVm, User>(userVm));
            var result = _mapper.Map<User, UserDto>(ok);
            return Ok(result);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserVm userVm)
        {
            var ok = await _userRepository.UpdateUserAsynce(id, _mapper.Map<UserVm, User>(userVm));
            var result = _mapper.Map<User,UserDto>(ok);
            return Ok(result);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
           await _userRepository.DeleteUserAsync(id);
        }
    }
}
