using FluentValidation;
using ToDo.Application.DTOs;

namespace ToDo.Application.Validators;

public class CreateToDoTaskDtoValidator : AbstractValidator<CreateToDoTaskDto>
{
    public CreateToDoTaskDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required")
            .Must(priority => priority == "Low" || priority == "Medium" || priority == "High")
            .WithMessage("Priority must be 'Low', 'Medium', or 'High'");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required")
            .MaximumLength(50).WithMessage("Category cannot exceed 50 characters");

        RuleFor(x => x.DueDate)
            .Must(date => !date.HasValue || date.Value > DateTime.UtcNow)
            .WithMessage("Due date must be in the future");
    }
}

public class UpdateToDoTaskDtoValidator : AbstractValidator<UpdateToDoTaskDto>
{
    public UpdateToDoTaskDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required")
            .Must(priority => priority == "Low" || priority == "Medium" || priority == "High")
            .WithMessage("Priority must be 'Low', 'Medium', or 'High'");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required")
            .MaximumLength(50).WithMessage("Category cannot exceed 50 characters");

        RuleFor(x => x.DueDate)
            .Must(date => !date.HasValue || date.Value > DateTime.UtcNow)
            .WithMessage("Due date must be in the future");
    }
} 