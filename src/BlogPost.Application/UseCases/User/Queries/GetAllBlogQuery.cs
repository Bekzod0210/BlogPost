using BlogPost.Application.Abstactions;
using BlogPost.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.User.Queries
{
    public class GetAllBlogQuery : IQuery<List<BlogViewModel>>
    {
    }

    public class GetAllBlogQueryHandler : IQueryHandler<GetAllBlogQuery, List<BlogViewModel>>
    {
        private readonly IAppDbContext _dbContext;

        public GetAllBlogQueryHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<BlogViewModel>> Handle(GetAllBlogQuery command, CancellationToken cancellationToken)
        {
            var posts = _dbContext.Posts;
            return await _dbContext.Blogs
                .Select(x => new BlogViewModel()
                {
                    Author = x.Author,
                    CreatedAt = x.CreatedAt,
                    Description = x.Description,
                    Name = x.Name,
                    UserNumber = x.UserNumber,
                    Posts = posts.Where(p => p.BlogId == x.Id)
                    .Select(x => new PostViewModel()
                    {
                        CreatedAt = x.CreatedAt,
                        Status = x.Status,
                        Title = x.Title,
                        Body = x.Body,
                    }).ToList(),
                })
                .ToListAsync(cancellationToken);
        }
    }
}
