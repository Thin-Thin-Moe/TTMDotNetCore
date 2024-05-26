using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMDotNetCore.ConsoleAppRefitExamples;

public interface IBlogApi
{
    [Get("/api/Blog")]
    Task<List<BlogModel>> GetBlogs();

    [Get("/api/Blog/{id}")]
    Task<BlogModel> GetBlog(int id);

    [Post("/api/Blog")]
    Task<string> CreateBlog(BlogModel blog);

    [Put("/api/Blog/{id}")]
    Task<string> UpdateBlog(int id, BlogModel blog);

    [Delete("/api/Blog/{id}")]
    Task<string> DeleteBlog(int id);
}

public class BlogModel
{
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }
}
