using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Application.DTOs;
using ToDo.Application.Interfaces;

namespace ToDo.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IToDoTaskService _taskService;

    public TasksController(IToDoTaskService taskService)
    {
        _taskService = taskService;
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            throw new Exception("User ID not found in token");

        return int.Parse(userIdClaim);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoTaskDto>>> GetTasks([FromQuery] TaskFilterDto filter)
    {
        var userId = GetUserId();
        var tasks = await _taskService.GetTasksAsync(filter, userId);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoTaskDto>> GetTask(int id)
    {
        var userId = GetUserId();
        var task = await _taskService.GetTaskByIdAsync(id, userId);
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<ToDoTaskDto>> CreateTask(CreateToDoTaskDto createTaskDto)
    {
        var userId = GetUserId();
        var task = await _taskService.CreateTaskAsync(createTaskDto, userId);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, UpdateToDoTaskDto updateTaskDto)
    {
        var userId = GetUserId();
        await _taskService.UpdateTaskAsync(id, updateTaskDto, userId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var userId = GetUserId();
        await _taskService.DeleteTaskAsync(id, userId);
        return NoContent();
    }

    [HttpPatch("{id}/toggle")]
    public async Task<IActionResult> ToggleTaskCompletion(int id)
    {
        var userId = GetUserId();
        await _taskService.ToggleTaskCompletionAsync(id, userId);
        return NoContent();
    }
} 