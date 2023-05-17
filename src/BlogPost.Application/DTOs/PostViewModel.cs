namespace BlogPost.Application.DTOs
{
    public class PostViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; } = string.Empty;
        public string? BlogName { get; set; } = string.Empty;
    }
}
