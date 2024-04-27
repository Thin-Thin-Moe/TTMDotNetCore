using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMDotNetCore.ConsoleApp;

internal class EFCoreExample
{
    private readonly AppDbContext db = new AppDbContext();
    public void Run()
    {
        // Read();
        // Edit(1);
        // Create("title 5", "author 5", "content 5");
        // Update(7,"title 7", "author 7", "content 7");
        Delete(5);
    }

    private void Read()
    {
        var lst = db.Blogs.ToList();

        foreach (BlogDto item in lst)
        {
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("------------------------");
        }

    }

    private void Edit(int id)
    {
        var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item == null)
        {
            Console.WriteLine("No data found.");
            return;
        }
        Console.WriteLine(item.BlogId);
        Console.WriteLine(item.BlogTitle);
        Console.WriteLine(item.BlogAuthor);
        Console.WriteLine(item.BlogContent);
    }

    private void Create(string title, string author, string content)
    {
        var item = new BlogDto
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        db.Blogs.Add(item);

        int result = db.SaveChanges();
        string message = result > 0 ? "Save Successful." : "Save Failed.";

        Console.WriteLine(message);
    }

    private void Update(int id, string title, string author, string content)
    {
        var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item == null)
        {
            Console.WriteLine("No data found.");
            return;
        }

        item.BlogTitle = title;
        item.BlogContent = content;
        item.BlogAuthor = author;

        int result = db.SaveChanges();
        string message = result > 0 ? "Update Successful." : "Update Failed.";

        Console.WriteLine(message);
    }

    private void Delete(int id)
    {
        var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item == null)
        {
            Console.WriteLine("No data found.");
            return;
        }

        db.Blogs.Remove(item);

        int result = db.SaveChanges();
        string message = result > 0 ? "Delete Successful." : "Delet Failed.";

        Console.WriteLine(message);
    }
}
