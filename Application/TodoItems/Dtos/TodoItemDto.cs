using AutoMapper;
using Domain.Entities;
using Core.Enums;
using System.Collections.Generic;
using Application.Common.Mappings;

namespace Application.TodoItems.Dtos
{
    public class TodoItemDto : IMapFrom<TodoItem>
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public PriorityEnum Priority { get; set; }
        public string Note { get; set; }
        public string BackgroundColor { get; set; }
        public ICollection<string> Tags { get; set; } = new List<string>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItem, TodoItemDto>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => s.Priority))
                .ForMember(d => d.BackgroundColor, opt => opt.MapFrom(s => s.BackgroundColor))
                .ReverseMap();
        }
    }
}
