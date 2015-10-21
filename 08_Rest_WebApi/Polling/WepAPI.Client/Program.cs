using System;
using System.Net.Http;

namespace WepAPI.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:2000/api/polls/")
            };

            HttpResponseMessage poll = client.GetAsync("2").Result;
            Console.WriteLine(poll.Content.ReadAsStringAsync().Result);
        }
    }
}
