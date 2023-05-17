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
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Blog>? Blogs { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
