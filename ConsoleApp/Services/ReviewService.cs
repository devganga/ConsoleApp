using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consoleApp.Services.Interface;

namespace consoleApp.Services
{
    public class Review
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int? Rating { get; set; }
    }
    public class ReviewService : IRunService
    {
        private readonly IHttpClientHelper<Review> httpClient;

        public ReviewService(IHttpClientHelper<Review> httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task Run()
        {
            Enumerable.Range(1, 100).ToList().ForEach(async x =>
            {
                var review = new Review { Author = "test", Title = $"Title{x}", Rating = 3 };
                await httpClient.PostRequest("http://localhost:5007/api/Reviews", review, default);
            });
            await Task.CompletedTask;
            //return await Task.CompletedTask;
            //return await Task.FromResult(0);
            //throw new NotImplementedException();
        }
    }
}
