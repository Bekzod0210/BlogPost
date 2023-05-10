using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Application.Abstactions
{
    public interface IHashService
    {
        string GetHash(string password);
    }
}
