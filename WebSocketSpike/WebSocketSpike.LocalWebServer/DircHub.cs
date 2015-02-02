using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace WebSocketSpike.LocalWebServer
{
    public class DircHub : Hub
    {
        public void Send(string user, string platform, string message)
        {
            Console.WriteLine("Broadcast message \"{0}\" from {1} to others.", message, Context.ConnectionId);
            Clients.Others.broadcastMessage(user, platform, message);
            Clients.Others.broadcastMessage(Context.ConnectionId, user, platform, message);
        }

        public override Task OnConnected()
        {
            Console.WriteLine("Connection from {0}", Context.ConnectionId);
            Clients.Caller.broadcastMessage("DircHub", "GNU/Linux", "Welcome. Your connectionId is " + Context.ConnectionId);
            Clients.Others.broadcastConnect(Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine("OnDisconnected {0}, from {1}", stopCalled, Context.ConnectionId);
            Clients.Others.broadcastDisconnect(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            Console.WriteLine("OnReconnected from {0}", Context.ConnectionId);
            Clients.Others.broadcastReconnect(Context.ConnectionId);
            return base.OnReconnected();
        }
    }
}