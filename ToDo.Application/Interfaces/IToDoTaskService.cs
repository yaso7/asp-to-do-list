using ToDo.Application.DTOs;

namespace ToDo.Application.Interfaces;

public interface IToDoTaskService
{
    Task<ToDoTaskDto> CreateTaskAsync(CreateToDoTaskDto createTaskDto, int userId);
    Task<ToDoTaskDto> GetTaskByIdAsync(int id, int userId);
    Task<IEnumerable<ToDoTaskDto>> GetTasksAsync(TaskFilterDto filter, int userId);
    Task UpdateTaskAsync(int id, UpdateToDoTaskDto updateTaskDto, int userId);
    Task DeleteTaskAsync(int id, int userId);
    Task ToggleTaskCompletionAsync(int id, int userId);
} 