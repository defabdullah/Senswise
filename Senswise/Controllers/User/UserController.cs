using Microsoft.AspNetCore.Mvc;
using Senswise.Context.Entity;
using Senswise.Services.Abstractions;

namespace Senswise.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<UserResponse>>> GetListOfUsersUser()
        {
            try
            {
                var response = await _userService.GetListOfUsersAsync();
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(int id)
        {
            try
            {
                var response = await _userService.GetUserAsync(id);
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserRequest user)
        {
            try
            {
                var response = await _userService.UpdateUserAsync(id, user);
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> CreateUser(UserRequest user)
        {
            try
            {
                var response = await _userService.CreateUserAsync(user);
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

    }
}
