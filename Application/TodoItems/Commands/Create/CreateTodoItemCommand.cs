using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoItems.Commands.Create
{
    public class CreateTodoItemCommand : IRequest<int>
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string BackgroundColor { get; set; }
        public List<string> Tags { get; set; }
        public int ListId { get; set; }
    }


}
