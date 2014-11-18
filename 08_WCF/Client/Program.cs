using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
//using Contracts;
using Client.AccumulateProxies;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //const string address = "http://localhost:9000/Accumulate";
            //Binding binding = new WSHttpBinding();


            //IAccumulate proxy = null;

            //var channelFactory = 
            //    //new ChannelFactory<IAccumulate>(binding, address);
            //    new ChannelFactory<IAccumulate>("BasicHttpBinding_IAccumulate");
            //    // new ChannelFactory<IAccumulate>("WSAccumulate");
            //   // new ChannelFactory<IAccumulate>("BasicAccumulate");


            //proxy = channelFactory.CreateChannel();

            var proxy = new AccumulateClient("BasicHttpBinding_IAccumulate");

            for (int i = 0; i < 10; i++)
            {
                proxy.Add(1);
            }
        }
    }
}











