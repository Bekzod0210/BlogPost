using BlogPost.Application.Abstactions;
using BlogPost.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.User.Commands
{
    public class CreatePostCommand : ICommand<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int BlogId { get; set; }
    }

    public class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, int>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public CreatePostCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreatePostCommand command, CancellationToken cancellationToken)
        {
            var blog = await _dbContext.Blogs.FirstOrDefaultAsync(x => x.Id == command.BlogId);
            var post = new Post();


            if (command.BlogId == 0)
            {
                post = new Post()
                {
                    Title = command.Title,
                    Body = command.Body,
                    Status = command.Status,
                    CreatedAt = DateTime.UtcNow,
                    UserId = _currentUserService.UserId,
                };
            }
            else if (_currentUserService.UserId == blog!.UserId)
            {
                post = new Post()
                {
                    Title = command.Title,
                    Body = command.Body,
                    Status = command.Status,
                    CreatedAt = DateTime.UtcNow,
                    UserId = _currentUserService.UserId,
                    BlogId = command.BlogId,
                };
            }
            else
            {
                throw new Exception("Yo cannot write to others blog!");
            }

            await _dbContext.Posts.AddAsync(post, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return post.Id;
        }
    }
}
