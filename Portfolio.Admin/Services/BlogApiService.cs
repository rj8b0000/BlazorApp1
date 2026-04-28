using System.Net.Http.Headers;
using Portfolio.Admin.Models.Blog;

namespace Portfolio.Admin.Services;

public class BlogApiService
{
    private readonly IHttpClientFactory _factory;
    public BlogApiService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task CreateBlogAsync(CreateBlogRequest request)
    {
        var client = _factory.CreateClient("PortfolioAPI");
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(request.Title), "Title");
        formData.Add(new StringContent(request.HtmlContent), "HtmlContent");
        formData.Add(new StringContent(request.ShortDescription), "ShortDescription");
        formData.Add(new StringContent(request.Tags), "Tags");
        formData.Add(new StringContent(request.IsPublished.ToString()), "IsPublished");

        if (request.CoverImage != null)
        {
            var stream = request.CoverImage.OpenReadStream(10_000_000);
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(request.CoverImage.ContentType);
            formData.Add(fileContent, "CoverImage", request.CoverImage.Name);
        }
        
        var response = await client.PostAsync("api/blog", formData);
        var content = await response.Content.ReadAsStringAsync();

        Console.WriteLine(content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task<List<BlogResponse>> GetBlogsAsync()
    {
        var client = _factory.CreateClient("PortfolioAPI");
        var response = await client.GetAsync("api/blog");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(content);
        
        return System.Text.Json.JsonSerializer.Deserialize<List<BlogResponse>>(content,
            new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        )?? new List<BlogResponse>();
    }
    
    public async Task<BlogResponse> GetBlogByIdAsync(Guid id)
    {
        var client = _factory.CreateClient("PortfolioAPI");

        var response = await client.GetAsync($"api/blog/id/{id}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(content);

        return System.Text.Json.JsonSerializer.Deserialize<BlogResponse>(
            content,
            new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
    }
    public async Task DeleteBlogAsync(Guid id)
    {
        var client = _factory.CreateClient("PortfolioAPI");

        var response = await client.DeleteAsync($"api/blog/{id}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(content);
    }
    
    public async Task UpdateBlogAsync(Guid id, CreateBlogRequest request)
    {
        var client = _factory.CreateClient("PortfolioAPI");

        var formData = new MultipartFormDataContent();

        formData.Add(new StringContent(request.Title), "Title");
        formData.Add(new StringContent(request.HtmlContent), "HtmlContent");
        formData.Add(new StringContent(request.ShortDescription), "ShortDescription");
        formData.Add(new StringContent(request.Tags), "Tags");
        formData.Add(new StringContent(request.IsPublished.ToString()), "IsPublished");

        if (request.CoverImage != null)
        {
            var stream = request.CoverImage.OpenReadStream(10_000_000);

            var fileContent = new StreamContent(stream);

            fileContent.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(request.CoverImage.ContentType);

            formData.Add(fileContent, "CoverImage", request.CoverImage.Name);
        }

        var response = await client.PutAsync($"api/blog/{id}", formData);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(content);
    }
}