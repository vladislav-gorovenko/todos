using AutoMapper;
using ToDoApp.DTOs.todos;
using ToDoApp.Entities;

namespace ToDoApp.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoEntity, TodoDto>();
        CreateMap<CreateTodoDto, TodoEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsCompleted, opt => opt.Ignore());
        CreateMap<UpdateTodoDto, TodoEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
    }
}