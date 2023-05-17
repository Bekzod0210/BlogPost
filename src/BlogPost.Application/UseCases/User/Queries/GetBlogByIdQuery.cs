using BlogPost.Application.Abstactions;
using BlogPost.Application.DTOs;
using BlogPost.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.User.Queries
{
    public class GetBlogByIdQuery : IQuery<BlogViewModel>
    {
        public int Id { get; set; }
    }

    public class GetBlogByIdQueryHandler : IQueryHandler<GetBlogByIdQuery, BlogViewModel>
    {
        private readonly IAppDbContext _dbContext;

        public GetBlogByIdQueryHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogViewModel> Handle(GetBlogByIdQuery command, CancellationToken cancellationToken)
        {
            var blog = await _dbContext.Blogs.FirstOrDefaultAsync(x => x.Id == command.Id);
            var posts = _dbContext.Posts;

            if (blog == null)
            {
                throw new EntityNotFoundException(nameof(Domain.Entities.Blog));
            }

            return new BlogViewModel()
            {
                Author = blog.Author,
                CreatedAt = blog.CreatedAt,
                Description = blog.Description,
                Name = blog.Name,
                UserNumber = blog.UserNumber,
                Posts = posts.Where(p => p.BlogId == blog.Id).Select(x => new PostViewModel()
                {
                    CreatedAt = x.CreatedAt,
                    Status = x.Status,
                    Title = x.Title,
                    Body = x.Body,
                }).ToList(),
            };
        }
    }
}
