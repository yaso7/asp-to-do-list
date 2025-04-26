namespace ToDo.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}

public class CreateUserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class AuthResponseDto
{
    public string Token { get; set; }
    public UserDto User { get; set; }
} 