using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var userDto = _userService.GetUserById(userId);
            if (userDto == null)
            {
                return NotFound();
            }
            return Ok(userDto);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var usersDto = _userService.GetAllUsers();
            return Ok(usersDto);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserDto userDto)
        {
            _userService.AddUser(userDto);
            return CreatedAtAction(nameof(GetUserById), new { userId = userDto.UserId }, userDto);
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            if (userId != userDto.UserId)
            {
                return BadRequest();
            }

            _userService.UpdateUser(userDto);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
            return NoContent();
        }
}
