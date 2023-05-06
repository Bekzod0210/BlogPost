using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Domain.Enums;

namespace BlogPost.Domain.Entities
{
    public class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
            Posts = new HashSet<Post>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
