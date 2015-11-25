using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitGotchas
{

   
    
    class Request
    {
        public Uri Uri;
        public byte[] Content;
    }
    class Program
    {
        static void Main(string[] args)
        {
            // GotchaOne();
            // GotchaTwo();
            // GotchaThree();
            GotchaFour().Wait();
        }

        private static async Task  GotchaFour()
        {
            Task processFileAsync = null;

            try
            {
                processFileAsync = ProcessFilesAsync(@"C:\users\administrator");
                await processFileAsync;

                //await ProcessFilesAsync(@"C:\users\administrator");
            }
            catch (AggregateException errors)
            {
                foreach (Exception error in processFileAsync.Exception.Flatten().InnerExceptions)
                {
                    Console.WriteLine(error.Message);
                }
            }
        }

        private static Task ProcessFilesAsync(string directory)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (FileInfo fi in new DirectoryInfo(directory).GetFiles())
                {
                    Task.Factory.StartNew(() =>
                    {
                        File.OpenRead(fi.FullName).Close();

                    }, TaskCreationOptions.AttachedToParent);
                }
            });
        }

        private static void GotchaThree()
        {
            var requests =
                new List<Request>()
                {
                    new Request() {Uri = new Uri("http://www.rocksolidknowledge.com")},
                    new Request() {Uri = new Uri("http://www.bbc.co.uk")},
                    new Request() {Uri = new Uri("http://www.cia.gov")}
                };

            //requests.ForEach(async request =>
            //{
            //    var client = new WebClient();
            //    Console.WriteLine("Downloading {0}", request.Uri);
            //    request.Content = await client.DownloadDataTaskAsync(request.Uri);
            //    Console.WriteLine("Downloaded {0}", request.Uri);
            //});

            Task.WaitAll(requests.Select(async request =>
            {
                var client = new WebClient();
                Console.WriteLine("Downloading {0}", request.Uri);
                request.Content = await client.DownloadDataTaskAsync(request.Uri);
                Console.WriteLine("Downloaded {0}", request.Uri);
            }).ToArray());


            Console.WriteLine("All done..??");

            requests.ForEach(r => Console.WriteLine(r.Content.Length));
        }

        private static void GotchaTwo()
        {

          

            try
            {
                UploadLogFilesAsync("http://www.rocksolidknowledge2.com");
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }



            Console.WriteLine("All done");
           Console.ReadLine();
           
        }

        // If this method does not return a task
        // then the exception is just re-thrown if no catcher boom
        // If it does return a task the exception is wrapped by the Task
        //
        private static async void UploadLogFilesAsync(string uri)
        {
           var client = new WebClient();
            string sourceDirectory = @"C:\temp\GNET\slides";


            foreach (FileInfo logFile in new DirectoryInfo(sourceDirectory).GetFiles("*.pptx"))
            {
                Console.WriteLine("Uploading {0}", logFile);
                await client.UploadFileTaskAsync(uri, logFile.FullName);

            }
        }

        private static void GotchaOne()
        {
            Task t = DoItAsync();

            Console.WriteLine("Doing it async...");

            t.Wait();

            Console.WriteLine("All done");
        }

        public static async Task DoItAsync()
        {
            Thread.Sleep(5000);
            Console.WriteLine("done it..");
        }
    }
}
