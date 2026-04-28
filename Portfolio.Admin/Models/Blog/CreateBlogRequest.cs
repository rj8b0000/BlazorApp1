using Microsoft.AspNetCore.Components.Forms;

namespace Portfolio.Admin.Models.Blog;

public class CreateBlogRequest
{
    public string Title { get; set; } = "";
    public string HtmlContent { get; set; } = "";
    public string ShortDescription { get; set; } = "";
    public string Tags { get; set; } = "";
    public bool IsPublished { get; set; } 
    public IBrowserFile? CoverImage { get; set; }
}