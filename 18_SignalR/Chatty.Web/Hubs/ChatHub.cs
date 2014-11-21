using System.Diagnostics;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Chatty.Web.Hubs
{
    public class ChatHub : Hub
    {
        [HubMethodName("connect")]
        public void Connect(string name)
        {
            Debug.WriteLine("{0} has connected", name);

            Clients.AllExcept(this.Context.ConnectionId)
                .newClientConnected(name);
        }

        [HubMethodName("sendChat")]
        public void SendChat(string from, string content)
        {
            Clients.All.onNewChat(from, content);
        }
    }
}