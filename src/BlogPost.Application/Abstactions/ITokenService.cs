using System.Security.Claims;

namespace BlogPost.Application.Abstactions
{
    public interface ITokenService
    {
        string GetAccessToken(Claim[] claims);
    }
}
