using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalR_StackOverflow.Hubs
{
    public class AdminHub : Hub
    {
        public void BroadcastNotifications(string message)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AdminHub>();
            context.Clients.All.receiveNotification(message, (new Random()).Next(0, 40));
        }
    }
}