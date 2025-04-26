using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.DTOs;
using ToDo.Application.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Infrastructure.Data;
using ToDo.Infrastructure.Auth;

namespace ToDo.Application.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly JwtSettings _jwtSettings;

    public UserService(ApplicationDbContext context, IMapper mapper, JwtSettings jwtSettings)
    {
        _context = context;
        _mapper = mapper;
        _jwtSettings = jwtSettings;
    }

    public async Task<AuthResponseDto> RegisterAsync(CreateUserDto createUserDto)
    {
        if (await _context.Users.AnyAsync(u => u.Username == createUserDto.Username))
            throw new Exception("Username already exists");

        if (await _context.Users.AnyAsync(u => u.Email == createUserDto.Email))
            throw new Exception("Email already exists");

        var user = _mapper.Map<User>(createUserDto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = JwtTokenGenerator.GenerateToken(user, _jwtSettings);
        return new AuthResponseDto
        {
            Token = token,
            User = _mapper.Map<UserDto>(user)
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        var token = JwtTokenGenerator.GenerateToken(user, _jwtSettings);
        return new AuthResponseDto
        {
            Token = token,
            User = _mapper.Map<UserDto>(user)
        };
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            throw new Exception("User not found");

        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task UpdateUserAsync(int id, CreateUserDto updateUserDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            throw new Exception("User not found");

        if (await _context.Users.AnyAsync(u => u.Username == updateUserDto.Username && u.Id != id))
            throw new Exception("Username already exists");

        if (await _context.Users.AnyAsync(u => u.Email == updateUserDto.Email && u.Id != id))
            throw new Exception("Email already exists");

        _mapper.Map(updateUserDto, user);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            throw new Exception("User not found");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
} 