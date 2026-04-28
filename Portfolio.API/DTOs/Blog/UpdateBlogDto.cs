namespace Portfolio_Backend.DTOs.Blog;

public class UpdateBlogDto
{
    public string Title { get; set; } = default!;
    public string HtmlContent { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;
    public string Tags { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
}