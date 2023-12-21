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

    [HttpPost]
    public IActionResult AddUser([FromBody] UserDto userDto)
    {
        try
        {
            var result =_userService.AddUser(userDto);
            return CreatedAtAction(nameof(GetUserById), new { result.Username }, userDto);
        }
        catch (Exception ex)
        {
            // Log the exception if needed
            return BadRequest();

            // Throw an exception to indicate failure
            throw new Exception("Failed to add user: " + ex.Message);
        }
    }

    [HttpGet("{userId}")]
    public IActionResult GetUserById(string userId)
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

    [HttpPut("{userId}")]
    public IActionResult UpdateUser(string userId, [FromBody] UserDto userDto)
    {
        _userService.UpdateUser(userDto,userId);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(string userId)
    {
        _userService.DeleteUser(userId);
        return NoContent();
    }
}
