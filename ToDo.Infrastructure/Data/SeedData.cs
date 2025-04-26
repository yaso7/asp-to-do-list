using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Entities;

namespace ToDo.Infrastructure.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Ensure database is created and migrated
        await context.Database.MigrateAsync();

        // Check if we already have data
        if (await context.Users.AnyAsync())
            return;

        // Seed users
        var owner = new User
        {
            Username = "owner",
            Email = "owner@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Owner123!"),
            Role = "Owner",
            CreatedAt = DateTime.UtcNow
        };

        var guest = new User
        {
            Username = "guest",
            Email = "guest@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Guest123!"),
            Role = "Guest",
            CreatedAt = DateTime.UtcNow
        };

        context.Users.AddRange(owner, guest);
        await context.SaveChangesAsync();

        // Seed tasks
        var tasks = new List<ToDoTask>
        {
            new()
            {
                Title = "Complete Project Documentation",
                Description = "Write comprehensive documentation for the ToDo API project",
                Priority = "High",
                Category = "Documentation",
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(7),
                UserId = owner.Id
            },
            new()
            {
                Title = "Implement Unit Tests",
                Description = "Add unit tests for all services and controllers",
                Priority = "Medium",
                Category = "Testing",
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(5),
                UserId = owner.Id
            },
            new()
            {
                Title = "Review Code",
                Description = "Review and refactor code for better performance",
                Priority = "Low",
                Category = "Code Review",
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(3),
                UserId = guest.Id
            }
        };

        context.Tasks.AddRange(tasks);
        await context.SaveChangesAsync();
    }
} 