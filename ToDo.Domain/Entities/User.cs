using System.ComponentModel.DataAnnotations;

namespace ToDo.Domain.Entities;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    [Required]
    public string Role { get; set; } // "Owner" or "Guest"
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<ToDoTask> Tasks { get; set; }
} 