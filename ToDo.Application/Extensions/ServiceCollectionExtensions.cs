using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.DTOs;
using ToDo.Application.Interfaces;
using ToDo.Application.Mapping;
using ToDo.Application.Services;
using ToDo.Application.Validators;

namespace ToDo.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));

        // Add FluentValidation
        services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
        services.AddScoped<IValidator<CreateToDoTaskDto>, CreateToDoTaskDtoValidator>();
        services.AddScoped<IValidator<UpdateToDoTaskDto>, UpdateToDoTaskDtoValidator>();

        // Add Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IToDoTaskService, ToDoTaskService>();

        return services;
    }
} 