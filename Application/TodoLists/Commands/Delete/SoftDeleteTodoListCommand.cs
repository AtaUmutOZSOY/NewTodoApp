using Application.Common.Exceptions;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoLists.Commands.Delete
{
    public class SoftDeleteTodoListCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public SoftDeleteTodoListCommand(int id)
        {
            Id = id;
        }
    }


    public class SoftDeleteTodoListCommandHandler : IRequestHandler<SoftDeleteTodoListCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public SoftDeleteTodoListCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SoftDeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoLists.FindAsync(request.Id);

            if (entity == null)
            {
                // Handle not found scenario
                return Unit.Value;
            }

            entity.Status = Core.Enums.EntityStatus.Inactive;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
