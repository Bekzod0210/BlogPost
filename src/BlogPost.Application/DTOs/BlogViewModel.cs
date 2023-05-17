namespace BlogPost.Application.DTOs
{
    public class BlogViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int UserNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PostViewModel>? Posts { get; set; }

    }
}
