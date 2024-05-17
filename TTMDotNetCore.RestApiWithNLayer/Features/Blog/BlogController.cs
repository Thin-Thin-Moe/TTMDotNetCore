using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TTMDotNetCore.RestApiWithNLayer.Db;

namespace TTMDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _blBlog;
        public BlogController()
        {
            _blBlog = new BL_Blog();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var lst = _blBlog.GetBlogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            var result = _blBlog.CreateBlog(blog);
            string message = result > 0 ? "Create Successful." : "Create failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);

            if (item is null) return NotFound("No data found.");

            var result = _blBlog.UpdateBlog(id, blog);
            string message = result > 0 ? "Update Successful." : "Update failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);

            if (item is null) return NotFound("No data found.");

            var result = _blBlog.PatchBlog(id, item);
            string message = result > 0 ? "Update Successful." : "Update failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _blBlog.DeleteBlog(id);
            string message = result > 0 ? "Delete Successful." : "Delete failed.";
            return Ok(message);
        }
    }
}
