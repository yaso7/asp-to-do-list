using AutoMapper;
using ToDo.Application.DTOs;
using ToDo.Domain.Entities;

namespace ToDo.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // ToDoTask mappings
        CreateMap<ToDoTask, ToDoTaskDto>();
        CreateMap<CreateToDoTaskDto, ToDoTask>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => false));
        CreateMap<UpdateToDoTaskDto, ToDoTask>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
} 