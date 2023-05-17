using System.Security.Claims;
using BlogPost.Application.Abstactions;
using Microsoft.AspNetCore.Http;

namespace BlogPost.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int UserId { get; set; }

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            if (int.TryParse(contextAccessor.HttpContext!.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value, out int value))
            {
                UserId = value;
            }
        }
    }
}
