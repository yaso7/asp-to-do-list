using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.DTOs;
using ToDo.Application.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Infrastructure.Data;

namespace ToDo.Application.Services;

public class ToDoTaskService : IToDoTaskService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ToDoTaskService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ToDoTaskDto> CreateTaskAsync(CreateToDoTaskDto createTaskDto, int userId)
    {
        var task = _mapper.Map<ToDoTask>(createTaskDto);
        task.UserId = userId;

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return _mapper.Map<ToDoTaskDto>(task);
    }

    public async Task<ToDoTaskDto> GetTaskByIdAsync(int id, int userId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
            throw new Exception("Task not found");

        return _mapper.Map<ToDoTaskDto>(task);
    }

    public async Task<IEnumerable<ToDoTaskDto>> GetTasksAsync(TaskFilterDto filter, int userId)
    {
        var query = _context.Tasks.Where(t => t.UserId == userId);

        if (filter.IsCompleted.HasValue)
            query = query.Where(t => t.IsCompleted == filter.IsCompleted.Value);

        if (!string.IsNullOrEmpty(filter.Priority))
            query = query.Where(t => t.Priority == filter.Priority);

        if (!string.IsNullOrEmpty(filter.Category))
            query = query.Where(t => t.Category == filter.Category);

        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            var searchTerm = filter.SearchTerm.ToLower();
            query = query.Where(t => 
                t.Title.ToLower().Contains(searchTerm) || 
                t.Description.ToLower().Contains(searchTerm));
        }

        var tasks = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ToDoTaskDto>>(tasks);
    }

    public async Task UpdateTaskAsync(int id, UpdateToDoTaskDto updateTaskDto, int userId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
            throw new Exception("Task not found");

        _mapper.Map(updateTaskDto, task);
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id, int userId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
            throw new Exception("Task not found");

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }

    public async Task ToggleTaskCompletionAsync(int id, int userId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
            throw new Exception("Task not found");

        task.IsCompleted = !task.IsCompleted;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
} 