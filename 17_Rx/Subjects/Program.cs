using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace Subjects
{
    class Program
    {
        static void Main(string[] args)
        {
            var carBooking = new PartialBooking("Car Hire");
            var flightBooking = new PartialBooking("Flight");
            var hotelBooking = new PartialBooking("Hotel");


            var query = from c in carBooking.Completed
                        from f in flightBooking.Completed
                        from h in hotelBooking.Completed
                        select new {Car = c.ElementName, Flight = f.ElementName, Hotel = h.ElementName};

            query.Subscribe(Console.WriteLine);

            Console.WriteLine("press enter to complete car");
            Console.ReadLine();
            carBooking.CompleteBooking();

            Console.WriteLine("press enter to complete flight");
            Console.ReadLine();
            flightBooking.CompleteBooking();
            
            Console.WriteLine("press enter to complete hotel");
            Console.ReadLine();
            hotelBooking.CompleteBooking();

            Console.WriteLine("press enter to exit");
            Console.ReadLine();
        }
    }

    class PartialBooking
    {
        public string ElementName { get; private set; }

        private Subject<PartialBooking> partialBooking = new Subject<PartialBooking>(); 
        public PartialBooking(string elementName)
        {

            ElementName = elementName;
        }

        public IObservable<PartialBooking> Completed { get { return partialBooking.AsObservable(); } } 

        public void CompleteBooking()
        {
            partialBooking.OnNext(this);
        }
    }
}
