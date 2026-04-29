using Microsoft.AspNetCore.Mvc;
using Portfolio_Backend.DTOs.Blog;
using Portfolio_Backend.Services.Interfaces;

namespace Portfolio_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly IBlogService _service;

    public BlogController(IBlogService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateBlogDto dto)
    {
        var id = await _service.CreateAsync(dto);

        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateBlogDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var blogs = await _service.GetAllAsync();

        return Ok(blogs);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var blog = await _service.GetBySlugAsync(slug);

        if (blog == null)
            return NotFound();

        return Ok(blog);
    }

    [HttpGet("id/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var blog = await _service.GetByIdAsync(id);
        if (blog == null)
            return NotFound();

        return Ok(blog);
    }
}