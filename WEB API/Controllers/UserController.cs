using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Validations;
using Data_Access_Layer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetUser();
            return Ok(users);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User_DTO newUser)
        {
            var result =await _userService.Register(newUser);
            return Ok();
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User_DTO user)
        {
            var result =await _userService.Login(user);
            return Ok(result);
        }


    }
}
