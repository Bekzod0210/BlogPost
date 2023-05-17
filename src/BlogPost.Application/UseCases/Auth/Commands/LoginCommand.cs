using System.Security.Claims;
using BlogPost.Application.Abstactions;
using BlogPost.Application.Exceptions;
using BlogPost.Domain.Enums;
using BlogPost.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.UseCases.Auth.Commands
{
    public class LoginCommand : ICommand<string>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AdminLoginCommandHandler : ICommandHandler<LoginCommand, string>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public AdminLoginCommandHandler(IAppDbContext dbContext, IHashService hashService, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _hashService = hashService;
            _tokenService = tokenService;
        }
        public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == command.UserName);

            if (user == null)
            {
                throw new LoginException(new EntityNotFoundException(nameof(Admin)));
            }

            if (user.PasswordHash != _hashService.GetHash(command.Password))
            {
                throw new LoginException();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            if (user.Role == Role.admin)
            {
                claims.Add(new Claim(ClaimTypes.Role, Role.admin.ToString()));
            }

            return _tokenService.GetAccessToken(claims.ToArray());
        }
    }
}
