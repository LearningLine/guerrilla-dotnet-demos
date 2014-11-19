using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ParentChild
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] urls =
            {
                "https://www.develop.com/technicalstaff/details/richard-blewett",
                "http://www.nationalgeographic.com",
                "http://stackoverflow.com",
                "http://xkcd.com",
                "http://bbc.co.uk"
            };

           // ParentChild(urls);
            var sw = Stopwatch.StartNew();

            var allDoneEvent = new CountdownEvent(urls.Length);
           
                foreach (var url in urls)
                {
                    var request = WebRequest.Create(url);
                    var ioTask = Task.Factory.FromAsync(request.BeginGetResponse(null, null), result =>
                    {
                        var resp = request.EndGetResponse(result);
                        Console.WriteLine("{0}: {1}", request.RequestUri, resp.ContentLength);
                        allDoneEvent.Signal();
                    });

                }

            allDoneEvent.Wait();
            Console.WriteLine(sw.Elapsed);

        }

        private static void ParentChild(string[] urls)
        {
            var sw = Stopwatch.StartNew();

            var downloadTask = Task.Factory.StartNew(() =>
            {
                foreach (var url in urls)
                {
                    var request = WebRequest.Create(url);
                    var ioTask = Task.Factory.FromAsync(request.BeginGetResponse(null, null), result =>
                    {
                        var resp = request.EndGetResponse(result);
                        Console.WriteLine("{0}: {1}", request.RequestUri, resp.ContentLength);
                    }, TaskCreationOptions.AttachedToParent);

                    //Task.Factory.StartNew(() =>
                    //{
                    //    var resp = request.GetResponse();
                    //    Console.WriteLine("{0}: {1}", request.RequestUri, resp.ContentLength);
                    //}, TaskCreationOptions.AttachedToParemnt);
                }
            });

            //Console.WriteLine("Engage the mike timer");
            //Console.ReadLine();

            downloadTask.Wait();

            Console.WriteLine("Done in " + sw.Elapsed);
        }
    }
}