using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMDotNetCore.ConsoleAppRefitExamples;

internal class RefitExample
{
    private readonly IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7054");

    public async Task RunAsync()
    {
        await ReadAsync();
        // await EditAsync(1);
        // await EditAsync(20);
        // await CreateAsync("title 7", "content 7", "author 7");
        // await UpdateAsync(7,"title 7", "content 7", "author 7");
        // await DeleteAsync(7);
    }

    private async Task ReadAsync()
    {
        var lst = await _service.GetBlogs();
        foreach (var item in lst)
        {
            Console.WriteLine($"Id => {item.BlogId}");
            Console.WriteLine($"Title => {item.BlogTitle}");
            Console.WriteLine($"Content => {item.BlogContent}");
            Console.WriteLine($"Author => {item.BlogAuthor}");
            Console.WriteLine("----------------");
        }
    }

    private async Task EditAsync(int id)
    {
        try
        {
            var item = await _service.GetBlog(id);
            Console.WriteLine($"Id => {item.BlogId}");
            Console.WriteLine($"Title => {item.BlogTitle}");
            Console.WriteLine($"Content => {item.BlogContent}");
            Console.WriteLine($"Author => {item.BlogAuthor}");
            Console.WriteLine("----------------");
        }
        catch (ApiException ex)
        {
            Console.WriteLine(ex.StatusCode.ToString());
            Console.WriteLine(ex.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task CreateAsync(string title, string content, string author)
    {
        BlogModel blog = new BlogModel()
        {
            BlogTitle = title,
            BlogContent = content,
            BlogAuthor = author
        };
        var message = await _service.CreateBlog(blog);
        Console.WriteLine(message);
    }

    private async Task UpdateAsync(int id, string title, string content, string author)
    {
        BlogModel blog = new BlogModel()
        {
            BlogTitle = title,
            BlogContent = content,
            BlogAuthor = author
        };
        var message = await _service.UpdateBlog(id, blog);
        Console.WriteLine(message);
    }

    private async Task DeleteAsync(int id)
    {
        var message = await _service.DeleteBlog(id);
        Console.WriteLine(message);
    }
}
