using BlogPost.Application.Abstactions;
using BlogPost.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.User.Commands
{
    public class DeleteBlogCommand : ICommand<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteBlogCommandHandler : ICommandHandler<DeleteBlogCommand, Unit>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public DeleteBlogCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteBlogCommand command, CancellationToken cancellationToken)
        {
            var blog = await _dbContext.Blogs.FirstOrDefaultAsync(x => x.Id == command.Id);
            var posts = await _dbContext.Posts.Where(x => x.BlogId == command.Id).ToListAsync();

            if (blog == null)
            {
                throw new EntityNotFoundException(nameof(Domain.Entities.Blog));
            }

            if (_currentUserService.UserId != blog.UserId)
            {
                throw new Exception("Error!");
            }

            _dbContext.Blogs.Remove(blog);
            _dbContext.Posts.RemoveRange(posts);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
