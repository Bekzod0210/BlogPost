using BlogPost.Application.Abstactions;
using BlogPost.Domain.Entities;
using BlogPost.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.User.Commands
{
    public class DeletePostCommand : ICommand<Unit>
    {
        public int Id { get; set; }
    }

    public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand, Unit>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public DeletePostCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeletePostCommand command, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == command.Id);

            if (post == null)
            {
                throw new EntityNotFoundException(nameof(Post));
            }

            if (_currentUserService.UserId != post.UserId)
            {
                throw new Exception("Error!");
            }

            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
