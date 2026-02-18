using Microsoft.AspNetCore.Mvc;
using LibraryManagement.BLL.Services;
using LibraryManagement.BLL.DTOs.Users;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService) => _userService = userService;

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<int>> CreateUser(UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = await _userService.CreateUserAsync(userCreateDto);
            return CreatedAtAction(nameof(GetUser), new { id = userId }, userId);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != userUpdateDto.Id) return BadRequest("User ID mismatch");

            await _userService.UpdateUserAsync(id, userUpdateDto);
            return NoContent();
        }

        // PATCH: api/users/5 (optional partial update)
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _userService.UpdateUserAsync(id, userUpdateDto);
            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
