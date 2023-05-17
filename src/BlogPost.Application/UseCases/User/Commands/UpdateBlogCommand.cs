using BlogPost.Application.Abstactions;
using BlogPost.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.User.Commands
{
    public class UpdateBlogCommand : ICommand<Unit>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateBlogCommandHandler : ICommandHandler<UpdateBlogCommand, Unit>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public UpdateBlogCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateBlogCommand command, CancellationToken cancellationToken)
        {
            var blog = await _dbContext.Blogs.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (blog == null)
            {
                throw new EntityNotFoundException(nameof(Domain.Entities.Blog));
            }

            if (_currentUserService.UserId != blog.UserId)
            {
                throw new Exception("Error!");
            }

            blog.Name = command.Name ?? blog.Name;
            blog.Description = command.Description ?? blog.Description;

            _dbContext.Blogs.Update(blog);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
