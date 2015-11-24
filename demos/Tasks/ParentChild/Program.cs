using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParentChild
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] earls = new string[]
            {
                "http://www.bbc.co.uk",
                "http://google.com",
                "http://stackoverflow.com",
                "http://youtube.com",
                "http://www.nsa.gov",
            };

            Stopwatch timer = Stopwatch.StartNew();

            //   SimpleParentChild(earls);

            Task parent =
              Task.Factory.StartNew(() =>
               {
                   foreach (string earl in earls)
                   {
                       WebRequest request =
                        WebRequest.Create(earl);

                       request
                           .GetResponseAsync()
                           .ContinueWith(prev =>
                           {
                               Console.WriteLine("{0}:{1}",
                                   request.RequestUri,
                                   prev.Result.ContentLength);
                           },TaskContinuationOptions.AttachedToParent);
                   }
               }); 

            //Console.ReadLine();
            parent.Wait();

          
            Console.WriteLine(timer.Elapsed);

        }

        private static void SimpleParentChild(string[] earls)
        {
            Task parent =
                Task.Factory.StartNew(() =>
                    //Task.Run(()=>
                {
                    foreach (string earl in earls)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            WebRequest request =
                                WebRequest.Create(earl);

                            WebResponse response = request.GetResponse();

                            Console.WriteLine("{0} : {1}",
                                request.RequestUri, response.ContentLength);
                        }, TaskCreationOptions.AttachedToParent);
                    }
                }); // Task.Run behaviour ,TaskCreationOptions.DenyChildAttach);

            //Console.ReadLine();
            parent.Wait();
        }
    }
}
