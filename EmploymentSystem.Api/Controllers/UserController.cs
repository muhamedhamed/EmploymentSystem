using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(
        ILogger<UserController> logger,
        IUserService userService)
    {
        _logger = logger ??
                    throw new ArgumentNullException(nameof(logger));
        _userService = userService ??
                    throw new ArgumentNullException(nameof(userService));
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var usersDto = _userService.GetAllUsers();
        _logger.LogInformation($"All Users is returned.");
        return Ok(usersDto);
    }

    [HttpGet("{userId}")]
    [ActionName("GetUserById")]
    public IActionResult GetUserById(string userId)
    {
        var userDto = _userService.GetUserById(userId);
        if (userDto == null)
        {
            _logger.LogInformation($"user with Id: {userId} not found");
            return NotFound();
        }
        _logger.LogInformation($"user with Id: {userId} is found and returned");
        return Ok(userDto);
    }

    [HttpPost, Route("register")]
    [ActionName("AddUser")]
    public IActionResult AddUser([FromBody] UserDto userDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Error while adding new user with Email :{userDto.Email}");
                return BadRequest();
            }

            var result = _userService.AddUser(userDto);
            _logger.LogInformation($"Successfully add new user with Email :{userDto.Email}");
            return CreatedAtAction(nameof(AddUser), userDto);
        }
        catch (Exception ex)
        {
            // Log the exception if needed
            _logger.LogError($"Error while adding new user with Email :{userDto.Email}");
            return BadRequest();
        }
    }

    [HttpPut("{userId}")]
    [ActionName("UpdateUser")]
    public IActionResult UpdateUser(string userId, [FromBody] UserDto userDto)
    {
        _userService.UpdateUser(userDto, userId);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    [ActionName("DeleteUser")]
    public IActionResult DeleteUser(string userId)
    {
        _userService.DeleteUser(userId);
        return NoContent();
    }

    [HttpGet,Route("login")]
    public IActionResult Login([FromBody] UserLoginDto userLoginDto)
    {
        var result = _userService.Authenticate(userLoginDto.Email, userLoginDto.Password);
        _logger.LogInformation($"User is returned.");
        if (result.Success)
        {
            return Ok(new { Token = result.Token });
        }

        return Unauthorized();
    }
}
