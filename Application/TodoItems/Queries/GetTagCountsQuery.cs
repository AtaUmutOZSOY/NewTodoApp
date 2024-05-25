using Core.Enums;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoItems.Queries
{
    public class GetTagCountsQuery : IRequest<IDataResult<List<TagCountDto>>>
    {
        public int ListId { get; set; }
    }

    public class GetTagCountsQueryHandler : IRequestHandler<GetTagCountsQuery, IDataResult<List<TagCountDto>>>
    {
        private readonly IApplicationDbContext _context;

        public GetTagCountsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IDataResult<List<TagCountDto>>> Handle(GetTagCountsQuery request, CancellationToken cancellationToken)
        {
            var tagCounts = await _context.TodoItemTags
                .Where(tag => tag.TodoItem.ListId == request.ListId && tag.TodoItem.Status == EntityStatus.Active)
                .GroupBy(tag => tag.Tag)
                .Select(group => new TagCountDto
                {
                    Tag = group.Key,
                    Count = group.Count()
                })
                .ToListAsync(cancellationToken);

            return new SuccessDataResult<List<TagCountDto>>(tagCounts);
        }
    }

    public class TagCountDto
    {
        public string Tag { get; set; }
        public int Count { get; set; }
    }

}
