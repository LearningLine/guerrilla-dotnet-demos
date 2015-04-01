using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient
{
    
    public class Person
    {
        public string fullName { get; set; }
        public int age { get; set; }
    }


    // {"fullName":"Brock","age":12}

    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();

            var p = new Person { fullName = "Bob", age = 12 };
            var result = client.PutAsJsonAsync("http://localhost:12968/people/1", p).Result;
            Console.WriteLine("{0}, {1}", (int)result.StatusCode, result.StatusCode);

            //var result = client.GetAsync("http://localhost:12968/people").Result;
            //Console.WriteLine("{0}, {1}", (int)result.StatusCode, result.StatusCode);
            //if (result.IsSuccessStatusCode)
            //{
            //    //var json = result.Content.ReadAsStringAsync().Result;
            //    ////Console.WriteLine(json);
                
            //    //var people = JsonConvert.DeserializeObject<Person[]>(json);

            //    var people = result.Content.ReadAsAsync<Person[]>().Result;

            //    foreach(var p in people)
            //    {
            //        Console.WriteLine(p.fullName + ", " + p.age);
            //    }


            //    //Newtonsoft.Json.JsonConvert.

            //    //var arr = JArray.Parse(json);
            //    //foreach (dynamic item in arr)
            //    //{
            //    //    Console.WriteLine(item.fullName);
            //    //    //Console.WriteLine(item["fullName"].ToString());
            //    //}
            //}
        }
    }
}
