using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    [ServiceContract]
    public interface IDo
    {
        [OperationContract]
        Task<int> DoThis();
    }


    public class DoService : IDo
    {
        public async Task<int> DoThis()
        {
            PrintThreadPoolUsage("DoThis start");
            
            var connection = new SqlConnection(@"server=.\SQLEXPRESS;database=master;integrated security=true");
            connection.Open();
            using (connection)
            {
                var cmd = new SqlCommand("SlowCall", connection);

                int result = (int) (await cmd.ExecuteScalarAsync());

                PrintThreadPoolUsage("DoThis after delay");

                return result;
            }
        }

        public static void PrintThreadPoolUsage(string label)
        {
            int ioThreads;
            int cpuThreads;

            ThreadPool.GetAvailableThreads(out cpuThreads, out ioThreads);

            Console.WriteLine("{0} {1},{2}", label, cpuThreads, ioThreads);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(DoService), new Uri("http://localhost:9000/Do"));

            host.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true });

            host.Open();
            Console.WriteLine("Server running..");
            while (true)
            {
                Console.ReadLine();
                DoService.PrintThreadPoolUsage("Main");
            }

        }
    }
}
