using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace WebSocketSpike.LocalWebServer
{
    public class MessageHub : Hub
    {
        public void Send(Message message)
        {
            Console.WriteLine("Broadcast message \"{0}\" from {1} to others.", message.Text, Context.ConnectionId);
            Clients.Others.broadcastMessage(message);
        }

        public override Task OnConnected()
        {
            
            Console.WriteLine("Connection from {0}", Context.ConnectionId);
            Clients.Caller.broadcastMessage(new Message { Text = "Welcome. Your connectionId is " + Context.ConnectionId });
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine("OnDisconnected {0}, from {1}", stopCalled, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            Console.WriteLine("OnReconnected from {0}", Context.ConnectionId);
            return base.OnReconnected();
        }
    }

    public class Message
    {
        public string Text { get; set; }
    }
}