using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    [ActionName("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve all users.");

            var usersDto =await  _userService.GetAllUsersAsync();
            if (usersDto == null)
            {
                _logger.LogWarning("No users found.");
                return NotFound("No users found.");
            }
            _logger.LogInformation("Successfully retrieved all users.");

            return Ok(usersDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while retrieving users: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpGet("{userId}")]
    [ActionName("GetUserById")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        try
        {
            _logger.LogInformation($"Attempting to retrieve user with ID: {userId}");

            if (!Guid.TryParse(userId, out _))
            {
                _logger.LogError("Invalid user ID format.");
                return BadRequest("Invalid user ID format.");
            }

            var userDto = await _userService.GetUserByIdAsync(userId);

            if (userDto == null)
            {
                _logger.LogWarning($"User with ID: {userId} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Successfully retrieved user with ID: {userId}");
            return Ok(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while retrieving user with ID {userId}: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpPost, Route("register")]
    [ActionName("AddUser")]
    public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
    {
        try
        {
            _logger.LogInformation($"Attempting to create a new user with email: {userDto.Email}");

            // Validate if the request body is null or empty
            if (userDto == null)
            {
                _logger.LogError("Invalid user data. Request body cannot be null.");
                return BadRequest("Invalid user data. Request body cannot be null.");
            }

            // Validate using model state
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state. Check the request data.");
                return BadRequest(ModelState);
            }

            var result = await _userService.AddUserAsync(userDto);

            _logger.LogInformation($"Successfully created a new user with ID: {result.UserId}");
            return CreatedAtAction(nameof(AddUser), result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to add user: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpPut("{userId}")]
    [ActionName("UpdateUser")]
    [Authorize(Policy ="User")]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserDto userDto)
    {
        try
        {
            _logger.LogInformation($"Attempting to update user with ID: {userId}");

            if (!Guid.TryParse(userId, out _))
            {
                _logger.LogError("Invalid user ID format.");
                return BadRequest("Invalid user ID format.");
            }

            // Check if the request body is null
            if (userDto == null)
            {
                _logger.LogError("Invalid user data. Request body cannot be null.");
                return BadRequest("Invalid user data. Request body cannot be null.");
            }

            // Validate using model state
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state. Check the request data.");
                return BadRequest(ModelState);
            }

            // Check if the vacancy exists
            var existingUser =await _userService.GetUserByIdAsync(userId);
            if (existingUser == null)
            {
                _logger.LogError($"User with ID {userId} not found.");
                return NotFound($"User with ID {userId} not found.");
            }

            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (existingUser.UserId != userIdFromToken)
            {
                _logger.LogError("User does not have permission to update this user info.");
                return Forbid("User does not have permission to update this user info.");
            }

            await _userService.UpdateUserAsync(userDto, userId);

            _logger.LogInformation($"Successfully updated user with ID: {userId}");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to update user: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpDelete("{userId}")]
    [ActionName("DeleteUser")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        try
        {
            _logger.LogInformation($"Attempting to delete user with ID: {userId}");

            if (!Guid.TryParse(userId, out _))
            {
                _logger.LogError("Invalid user ID format.");
                return BadRequest("Invalid user ID format.");
            }

            // Check if the user exists
            var existingUser =await _userService.GetUserByIdAsync(userId);
            if (existingUser == null)
            {
                _logger.LogError($"User with ID {userId} not found.");
                return NotFound($"User with ID {userId} not found.");
            }

            // Need to handle the not allowed expection fired here
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (existingUser.UserId != userIdFromToken)
            {
                _logger.LogError("User does not have permission to delete this user info.");
                return Unauthorized("User does not have permission to delete this user info.");
            }

            // Delete the user
           await _userService.DeleteUserAsync(userId);

            _logger.LogInformation($"Successfully deleted user with ID: {userId}");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to delete user: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpGet, Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        try
        {
            _logger.LogInformation($"Attempting to login user with email: {userLoginDto.Email}");

            var result =await _userService.AuthenticateAsync(userLoginDto.Email, userLoginDto.Password);
            if (result == null)
            {
                _logger.LogWarning($"User with email: {userLoginDto.Email} not found.");
                return NotFound();
            }
            _logger.LogInformation($"Successfully retrieved user with email: {userLoginDto.Email}");
            if (result.Success)
            {
                return Ok(new { Token = result.Token });
            }
            return Unauthorized();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while retrieving user with email {userLoginDto.Email}: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }
}
