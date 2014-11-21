using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace SignalrClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnection("http://localhost:38183/signalr");
            IHubProxy proxy = connection.CreateHubProxy("chatHub");

            connection.Start().Wait();

            proxy.Invoke<dynamic>("connect", "Andy").Wait();


        }
    }
}
