namespace Portfolio_Backend.Models.Entities;

public class Blog
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string CoverImageUrl { get; set; } = default!;
    public string HtmlContent { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;
    public string Tags { get; set; } = string.Empty;
    public bool IsPublished { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}