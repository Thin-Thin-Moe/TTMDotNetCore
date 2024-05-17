using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using TTMDotNetCore.ConsoleApp.Dtos;
using TTMDotNetCore.RestApi.Db;

namespace TTMDotNetCore.RestApi.Controllers
{
    // endpoint
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _content;
        public BlogController ()
        {
            _content = new AppDbContext();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var blogs = _content.Blogs.ToList();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var blog = _content.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                return NotFound("No data found.");
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _content.Blogs.Add(blog);
            var result = _content.SaveChanges();
            string message = result > 0 ? "Create Successful." : "Create failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _content.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogContent = blog.BlogContent;
            item.BlogAuthor = blog.BlogAuthor;

            var result = _content.SaveChanges();
            string message = result > 0 ? "Update Successful." : "Update failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _content.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                return NotFound("No data found.");
            }
            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }

            var result = _content.SaveChanges();
            string message = result > 0 ? "Update Successful." : "Update failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _content.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            _content.Blogs.Remove(item);
            var result = _content.SaveChanges();
            string message = result > 0 ? "Delete Successful." : "Delete failed.";
            return Ok(message);
        }

    }
}
