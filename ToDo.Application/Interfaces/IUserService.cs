using ToDo.Application.DTOs;

namespace ToDo.Application.Interfaces;

public interface IUserService
{
    Task<AuthResponseDto> RegisterAsync(CreateUserDto createUserDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    Task<UserDto> GetUserByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task UpdateUserAsync(int id, CreateUserDto updateUserDto);
    Task DeleteUserAsync(int id);
} 