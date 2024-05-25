// src/Application/Common/Mappings/MappingProfile.cs
using AutoMapper;
using Domain.Entities;
using Application.TodoItems.Dtos;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Command to Entity mapping
            CreateMap<CreateTodoItemCommand, TodoItem>();

            // Entity to DTO mapping
            CreateMap<TodoItem, TodoItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted))
                .ForMember(dest => dest.BackgroundColor, opt => opt.MapFrom(src => src.BackgroundColor))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));
        }
    }
}
