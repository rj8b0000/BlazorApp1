using Microsoft.EntityFrameworkCore;
using Portfolio_Backend.Data;
using Portfolio_Backend.DTOs.Blog;
using Portfolio_Backend.Helpers;
using Portfolio_Backend.Models.Entities;
using Portfolio_Backend.Services.Interfaces;

namespace Portfolio_Backend.Services.Implementations;

public class BlogService : IBlogService
{
       private readonly AppDbContext _context;
       private readonly IWebHostEnvironment _environment;

    public BlogService(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<Guid> CreateAsync(CreateBlogDto dto)
    {
        var slug = SlugHelpers.Generate(dto.Title);
        var imagePath = await FileUploadHelper.SaveFileAsync(
            dto.CoverImage,
            _environment.WebRootPath,
            "uploads/blogs"
        );
        var blog = new Blog
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Slug = slug,
            HtmlContent = dto.HtmlContent,
            ShortDescription = dto.ShortDescription,
            Tags = dto.Tags,
            IsPublished = dto.IsPublished,
            CoverImageUrl = imagePath,
            CreatedAt = DateTime.UtcNow
        };

        _context.Blogs.Add(blog);

        await _context.SaveChangesAsync();

        return blog.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateBlogDto dto)
    {
        var blog = await _context.Blogs.FindAsync(id);

        if (blog == null)
            throw new Exception("Blog not found");

        if (dto.CoverImage != null)
        {
            var imagePath = await FileUploadHelper.SaveFileAsync(
                dto.CoverImage,
                _environment.WebRootPath,
                "uploads/blogs"
            );
            blog.CoverImageUrl = imagePath;
        }

        blog.Title = dto.Title;
        blog.Slug = SlugHelpers.Generate(dto.Title);
        blog.HtmlContent = dto.HtmlContent;
        blog.ShortDescription = dto.ShortDescription;
        blog.Tags = dto.Tags;
        blog.IsPublished = dto.IsPublished;
        blog.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var blog = await _context.Blogs.FindAsync(id);

        if (blog == null)
            throw new Exception("Blog not found");

        _context.Blogs.Remove(blog);

        await _context.SaveChangesAsync();
    }

    public async Task<List<BlogResponseDto>> GetAllAsync()
    {
        return await _context.Blogs
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new BlogResponseDto
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                CoverImageUrl = x.CoverImageUrl,
                ShortDescription = x.ShortDescription,
                HtmlContent = x.HtmlContent,
                Tags = x.Tags,
                IsPublished = x.IsPublished,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<BlogResponseDto?> GetBySlugAsync(string slug)
    {
        return await _context.Blogs
            .Where(x => x.Slug == slug)
            .Select(x => new BlogResponseDto
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                CoverImageUrl = x.CoverImageUrl,
                ShortDescription = x.ShortDescription,
                HtmlContent = x.HtmlContent,
                Tags = x.Tags,
                IsPublished = x.IsPublished,
                CreatedAt = x.CreatedAt
            })
            .FirstOrDefaultAsync();
    }
    public async Task<BlogResponseDto?> GetByIdAsync(Guid id)
    {
        return await _context.Blogs
            .Where(x => x.Id == id)
            .Select(x => new BlogResponseDto
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                CoverImageUrl = x.CoverImageUrl,
                ShortDescription = x.ShortDescription,
                IsPublished = x.IsPublished,
                HtmlContent = x.HtmlContent,
                Tags = x.Tags,
                CreatedAt = x.CreatedAt,
            })
            .FirstOrDefaultAsync();
    }
}