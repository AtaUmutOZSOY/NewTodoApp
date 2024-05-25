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

        }
    }
}
