namespace Portfolio_Backend.DTOs.Blog;

public class BlogResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string CoverImageUrl { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public string HtmlContent { get; set; } = "";
    public string Tags { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}