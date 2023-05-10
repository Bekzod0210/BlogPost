using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlogPost.Application.Abstactions;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.Admin.Commands
{
    public class UserDeleteByIdCommand : ICommand<int>
    {
        public int Id { get; set; }
    }

    public class UserDeleteByIdCommandHandler : ICommandHandler<UserDeleteByIdCommand, int>
    {
        private readonly IAppDbContext _dbContext;

        public UserDeleteByIdCommandHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(UserDeleteByIdCommand command, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == command.Id);

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
