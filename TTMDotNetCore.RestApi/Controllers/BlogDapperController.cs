using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TTMDotNetCore.ConsoleApp.Dtos;

namespace TTMDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogModel> blogs = db.Query<BlogModel>(query).ToList();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var blog = FindById(id);

            if(blog is null)
            {
                return NotFound("No data found.");
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Create Successful." : "Create Failed.";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = FindById(id);

            if (item is null)
            {
                return NotFound("No data found.");
            }
            blog.BlogId = id;
            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [BlogTitle] = @BlogTitle
                  ,[BlogAuthor] = @BlogAuthor
                  ,[BlogContent] = @BlogContent
             WHERE BlogId = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Update Successful." : "Update Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindById(id);

            if (item is null)
            {
                return NotFound("No data found.");
            }
            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogTitle] =  @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += "[BlogContent] =  @BlogContent, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += "[BlogAuthor] =  @BlogAuthor, ";
            }
            if(conditions.Length == 0)
            {
                return NotFound("No data to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog] set {conditions} WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Update Successful." : "Update Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);

            if (item is null)
            {
                return NotFound("No data found.");
            }

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
            WHERE BlogId = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, new BlogModel { BlogId = id});
            string message = result > 0 ? "Delete Successful." : "Delete Failed.";
            return Ok(message);
        }

        private BlogModel? FindById(int id)
        {
            string query = "select * from tbl_blog where blogid = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var blog = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
            return blog;
        }
    }
}
