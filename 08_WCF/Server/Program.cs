using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace Server
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AccumulatorService : IAccumulate
    {
        private int total = 0;
        public void Add(int val)
        {
            total += val;
            Console.WriteLine(total);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string address = "http://localhost:9000/Accumulate";
            Binding binding = new WSHttpBinding();

            var host = new ServiceHost(typeof (AccumulatorService));
                //,new Uri(address));

            //host.AddServiceEndpoint(
            //    typeof(IAccumulate),
            //    binding,
            //    address);


            host.Open();

            host.Description.Endpoints.ToList()
                .ForEach(ep => Console.WriteLine("{0} {1}", ep.Binding.GetType().Name,
                    ep.Address));

            Console.WriteLine("Server running...");
            Console.ReadLine();
        }
    }
}











