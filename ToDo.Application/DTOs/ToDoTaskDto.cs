namespace ToDo.Application.DTOs;

public class ToDoTaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string Priority { get; set; }
    public string Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public int UserId { get; set; }
}

public class CreateToDoTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; }
    public string Category { get; set; }
    public DateTime? DueDate { get; set; }
}

public class UpdateToDoTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string Priority { get; set; }
    public string Category { get; set; }
    public DateTime? DueDate { get; set; }
}

public class TaskFilterDto
{
    public bool? IsCompleted { get; set; }
    public string Priority { get; set; }
    public string Category { get; set; }
    public string SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
} 