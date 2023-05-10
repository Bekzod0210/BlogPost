using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Application.Abstactions;
using XSystem.Security.Cryptography;

namespace BlogPost.Infrastructure.Services
{
    public class HashService : IHashService
    {
        public string GetHash(string value)
        {
            var sha256 = new SHA256Managed();
            var bytes = Encoding.UTF8.GetBytes(value);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
