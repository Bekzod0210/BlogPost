using BlogPost.Application.Abstactions;
using BlogPost.Domain.Entities;
using BlogPost.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.User.Commands
{
    public class UpdatePostCommand : ICommand<Unit>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Status { get; set; }
        public int? BlogId { get; set; }
    }

    public class UpdatePostCommandHandler : ICommandHandler<UpdatePostCommand, Unit>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public UpdatePostCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdatePostCommand command, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == command.Id);
            var blogs = _dbContext.Blogs
                .Where(x => x.UserId == _currentUserService.UserId)
                .Select(x => x.Id).ToList();

            blogs.Add(0);

            if (post == null)
            {
                throw new EntityNotFoundException(nameof(Post));
            }

            if (_currentUserService.UserId != post.UserId)
            {
                throw new Exception("Error!");
            }

            if (blogs.All(x => x != Convert.ToInt32(command.BlogId)))
            {
                throw new Exception("Yo cannot write to others blog!");
            }

            post.Title = command.Title ?? post.Title;
            post.Body = command.Body ?? post.Body;
            post.Status = command.Status ?? post.Status;
            post.BlogId = command.BlogId == 0 ? null : command.BlogId;

            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
