using System.Data;
using System.Data.SqlClient;
using TTMDotNetCore.ConsoleApp;

Console.WriteLine("Hello World!");
// nuget
// SqlConnection

// Ctrl + .
// F10
// F11

/* SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.DataSource = ".";
stringBuilder.InitialCatalog = "TTMDotNetCore";
stringBuilder.UserID = "sa";
stringBuilder.Password = "sa@123";
SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);

connection.Open();

string query = "select * from tbl_blog";
SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
sqlDataAdapter.Fill(dt);

foreach (DataRow dr in dt.Rows)
{
    Console.WriteLine("Blog Id => " + dr["BlogId"]);
    Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
    Console.WriteLine("Blog Content => " + dr["BlogContent"]);
}

connection.Close();*/

// AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
// adoDotNetExample.Read();
// adoDotNetExample.Create("title 2", "author 2", "content 2");
// adoDotNetExample.Create("title 3", "author 3", "content 3");
// adoDotNetExample.Update(1, "title 1", "author 1", "content 1");
// adoDotNetExample.Delete(3);
// adoDotNetExample.Edit(1);

DapperExample dapperExample = new DapperExample();
dapperExample.Run();

Console.ReadLine();