using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.DTOs;
using ToDo.Application.Interfaces;

namespace ToDo.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(CreateUserDto createUserDto)
    {
        var response = await _userService.RegisterAsync(createUserDto);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
    {
        var response = await _userService.LoginAsync(loginDto);
        return Ok(response);
    }

    [Authorize(Roles = "Owner")]
    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [Authorize(Roles = "Owner")]
    [HttpGet("users/{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [Authorize(Roles = "Owner")]
    [HttpPut("users/{id}")]
    public async Task<IActionResult> UpdateUser(int id, CreateUserDto updateUserDto)
    {
        await _userService.UpdateUserAsync(id, updateUserDto);
        return NoContent();
    }

    [Authorize(Roles = "Owner")]
    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
} 