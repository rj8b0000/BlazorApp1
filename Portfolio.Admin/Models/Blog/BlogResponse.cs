namespace Portfolio.Admin.Models.Blog;

public class BlogResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Slug { get; set; } = "";
    public string CoverImageUrl { get; set; } = "";
    public string ShortDescription { get; set; } = "";
    public bool IsPublished { get; set; }
    public string HtmlContent { get; set; }
    public string Tags { get; set; } 
    public DateTime CreatedAt { get; set; }
}