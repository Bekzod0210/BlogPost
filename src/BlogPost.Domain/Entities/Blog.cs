namespace BlogPost.Domain.Entities
{
    public class Blog
    {
        public Blog()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int UserNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
