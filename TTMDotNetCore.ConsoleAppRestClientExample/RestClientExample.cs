using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TTMDotNetCore.ConsoleAppRestClientExample
{
    internal class RestClientExample
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7190"));
        private readonly string _BlogEndpoint = "api/Blog";
        public async Task RunAsync()
        {
            // await ReadAsync();
            // await EditAsync(1);
            // await CreateAsync("title 6", "content 6", "author 6");
            await UpdateAsync(6, "title 6", "content 6", "author 6");
            // await DeleteAsyns(7);
        }

        private async Task ReadAsync()
        {
            /*RestRequest restRequest = new RestRequest(_BlogEndpoint);
            var response = await _client.GetAsync(restRequest);*/

            RestRequest restRequest = new RestRequest(_BlogEndpoint, Method.Get);
            var response = await _client.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
                foreach (var item in lst)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(item));
                    Console.WriteLine($"Title => {item.BlogTitle}");
                    Console.WriteLine($"Content => {item.BlogContent}");
                    Console.WriteLine($"Author => {item.BlogAuthor}");
                }
            }
        }

        private async Task EditAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_BlogEndpoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                var item = JsonConvert.DeserializeObject<BlogDto>(jsonStr)!;

                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Content => {item.BlogContent}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task CreateAsync(string title, string content, string author)
        {
            BlogDto blogDto = new BlogDto
            {
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
         
            RestRequest restRequest = new RestRequest(_BlogEndpoint, Method.Post);
            restRequest.AddJsonBody(blogDto);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }

        }

        private async Task UpdateAsync(int id, string title, string content, string author)
        {
            BlogDto blogDto = new BlogDto
            {
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
            RestRequest restRequest = new RestRequest($"{_BlogEndpoint}/{id}", Method.Put);
            restRequest.AddJsonBody(blogDto);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }

        }

        private async Task DeleteAsyns(int id)
        {
            RestRequest restRequest = new RestRequest($"{_BlogEndpoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
    }
}
