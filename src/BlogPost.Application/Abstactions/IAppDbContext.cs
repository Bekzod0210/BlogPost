using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Application.Abstactions
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Blog> Blogs { get; set; }
        DbSet<Post> Posts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
