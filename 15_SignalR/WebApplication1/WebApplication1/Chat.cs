using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Chat : Hub
    {
        public override System.Threading.Tasks.Task OnConnected()
        {
            //this.Groups.Add(this.Context.ConnectionId, "Admin");
            //Context.ConnectionId;
            //Context.User.Identity.Name;
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public void SendMessage(string message)
        {
            //this.Clients.Group("Admins").NotificationBlah();
            //this.Clients.Client("").
            this.Clients.All.GotMessage(message);
        }
    }
}