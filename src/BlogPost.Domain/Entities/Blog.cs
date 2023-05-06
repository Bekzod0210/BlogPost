using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Domain.Entities
{
    public class Blog
    {
        public Blog()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public int UserNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
