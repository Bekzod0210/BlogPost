using BlogPost.Application.Abstactions;
using BlogPost.Application.Exceptions;
using BlogPost.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.User.Commands
{
    public class CreateBlogCommand : ICommand<int>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class CretaeBlogCommandHandler : ICommandHandler<CreateBlogCommand, int>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public CretaeBlogCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }
        public async Task<int> Handle(CreateBlogCommand command, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (command.Name == null)
            {
                throw new NameException(nameof(Blog));
            }


            var blog = new Blog
            {
                Name = command.Name,
                Description = command.Description ?? "",
                CreatedAt = DateTime.UtcNow,
                Author = user.Name,
                ImageUrl = "www.gogle.com",
                UserId = user.Id,
            };

            await _dbContext.Blogs.AddAsync(blog, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return blog.Id;
        }
    }
}
