using BlogPost.Application.Abstactions;
using BlogPost.Application.DTOs;
using BlogPost.Domain.Entities;
using BlogPost.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.User.Queries
{
    public class GetPostById : IQuery<PostViewModel>
    {
        public int Id { get; set; }
    }

    public class GetPostByIdHandler : IQueryHandler<GetPostById, PostViewModel>
    {
        private readonly IAppDbContext _dbContext;

        public GetPostByIdHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PostViewModel> Handle(GetPostById query, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts.Include(u => u.User).Include(b => b.Blog).FirstOrDefaultAsync(x => x.Id == query.Id);

            if (post == null)
            {
                throw new EntityNotFoundException(nameof(Post));
            }

            var viewPost = new PostViewModel()
            {
                Title = post.Title,
                Body = post.Body,
                BlogName = post.Blog!.Name ?? "This post is not in blog",
                Author = post.User!.Name,
                Status = post.Status,
                CreatedAt = post.CreatedAt,
            };

            return viewPost;
        }
    }
}
