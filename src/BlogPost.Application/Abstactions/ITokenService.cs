using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Application.Abstactions
{
    public interface ITokenService
    {
        string GetAccessToken(Claim[] claims);
    }
}
