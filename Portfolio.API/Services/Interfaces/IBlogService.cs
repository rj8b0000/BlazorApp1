using Portfolio_Backend.DTOs.Blog;

namespace Portfolio_Backend.Services.Interfaces;

public interface IBlogService
{
    Task<Guid> CreateAsync(CreateBlogDto dto);
    Task UpdateAsync(Guid id, UpdateBlogDto dto);
    Task DeleteAsync(Guid id);
    Task<List<BlogResponseDto>> GetAllAsync();
    Task<BlogResponseDto?> GetBySlugAsync(string slug);
    Task<BlogResponseDto?> GetByIdAsync(Guid id);
}