namespace Portfolio_Backend.DTOs.Blog;

public class CreateBlogDto
{
    public string Title { get; set; } = default!;
    public string HtmlContent { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;
    public string Tags { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public IFormFile CoverImage { get; set; } = default!;
}