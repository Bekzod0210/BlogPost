using BlogPost.Application.Abstactions;
using BlogPost.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.User.Queries
{
    public class GetAllPostQuery : IQuery<List<PostViewModel>>
    {
    }

    public class GetAllPostQueryHandler : IQueryHandler<GetAllPostQuery, List<PostViewModel>>
    {
        private readonly IAppDbContext _dbContext;

        public GetAllPostQueryHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PostViewModel>> Handle(GetAllPostQuery query, CancellationToken cancellationToken)
        {
            var posts = await _dbContext.Posts
                .Select(x => new PostViewModel()
                {
                    Title = x.Title,
                    Body = x.Body,
                    BlogName = x.Blog!.Name ?? "This post is not in blog",
                    Author = x.User!.Name,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                })
                .ToListAsync(cancellationToken);

            return posts;
        }
    }
}
