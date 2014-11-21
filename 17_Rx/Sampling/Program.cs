using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Reactive;
using System.Reactive.Linq;

namespace Sampling
{
    class Program
    {
        static void Main(string[] args)
        {
            TemperatureSensor sensor = new TemperatureSensor(23);

            
            sensor.GetTemperatures().ToObservable()
                .Sample(TimeSpan.FromMilliseconds(100))
                .Buffer(new TimeSpan(0, 0, 5))
                .Subscribe(temps =>
                {
                    bool tooHigh = true;

                    Console.WriteLine("Processing {0} events", temps.Count);

                    foreach (var item in temps)
                    {
                        if (item < 24)
                        {
                            tooHigh = false;
                        }
                    }

                    if (tooHigh)
                    {
                        Console.WriteLine("Temperature too high ... Cooling");
                        sensor.Cool();
                    }
                    else
                    {
                        Console.WriteLine("Within Tolerance {0} Centigrade", temps.Average());
                    }
                },new Action<Exception>( error =>
                                             {
                                                 

                                             }));

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
