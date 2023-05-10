using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlogPost.Application.Abstactions;
using BlogPost.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.User.Command
{
    public class UserRegisterCommand : ICommand<int>
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserRegisterCommandHandler : ICommandHandler<UserRegisterCommand, int>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IHashService _hashService;

        public UserRegisterCommandHandler(IAppDbContext dbContext, IHashService hashService)
        {
            _dbContext = dbContext;
            _hashService = hashService;
        }
        public async Task<int> Handle(UserRegisterCommand command, CancellationToken cancellationToken)
        {            
            if(await _dbContext.Users.AnyAsync(x => x.UserName == command.UserName))
            {
                throw new UserNameExistException();
            }

            var user = new Domain.Entities.User()
            {
                Name = command.Name,
                UserName = command.UserName,
                PasswordHash = _hashService.GetHash(command.Password),
                CreatedAt = DateTime.UtcNow,
                Role = Domain.Enums.Role.user
            };


            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return user.Id;
        }
    }
}
