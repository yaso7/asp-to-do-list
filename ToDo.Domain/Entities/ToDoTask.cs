using System.ComponentModel.DataAnnotations;

namespace ToDo.Domain.Entities;

public class ToDoTask
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Title { get; set; }
    
    [StringLength(500)]
    public string Description { get; set; }
    
    public bool IsCompleted { get; set; }
    
    [Required]
    public string Priority { get; set; } // "Low", "Medium", "High"
    
    [Required]
    public string Category { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
} 