using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Reactive.Linq;

namespace HttpWinner
{
    public class NullDisposable : IDisposable
    {
        public static readonly NullDisposable Null = new NullDisposable();

        public void Dispose()
        {
            
        }
    }
    public class NullObservable<T> : IObservable<T>
    {
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return NullDisposable.Null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] urls = {
                               "http://www.bbc.co.uk/news",
                               "http://www.google.com",
                               "http://www.rocksolidknowledge.com",
                               "http://www.mi5.gov.uk",
                               "http://www.cia.gov",
                            };

            foreach (string url in urls)
            {
                string localUrl = url;

                WebRequest request = WebRequest.Create(localUrl);

            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}




