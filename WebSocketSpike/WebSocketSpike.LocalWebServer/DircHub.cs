using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace WebSocketSpike.LocalWebServer
{
    public class DircHub : Hub
    {
        private static List<DircUser> users = new List<DircUser>();

        public void Send(string userName, string platform, string message)
        {
            if (!users.Exists(u => u.ConnectionId == Context.ConnectionId))
            {
                AddNewUser(Context.ConnectionId, userName, platform);
            }

            message = CleanMessage(message);

            Console.WriteLine("Broadcast message \"{0}\" from {1} to others.", message, Context.ConnectionId);
            Clients.Others.broadcastMessage(userName, platform, message);
            Clients.Others.broadcastDircMessage(Context.ConnectionId, message);
        }

        public void SendDircMessage(string message)
        {
            if (!users.Exists(u => u.ConnectionId == Context.ConnectionId))
            {
                Console.WriteLine("Unauthorized user ({0}) attempted to send '{1}' to others.", Context.ConnectionId, message);
                return;
            }

            message = CleanMessage(message);

            Console.WriteLine("Broadcast message \"{0}\" from {1} to others.", message, Context.ConnectionId);
            Clients.Others.broadcastDircMessage(Context.ConnectionId, message);
        }

        private string CleanMessage(string message)
        {
            return Microsoft.JScript.GlobalObject.escape(message);
        }

        public void Register(string userName, string platform)
        {
            if (!users.Exists(u => u.ConnectionId == Context.ConnectionId))
            {
                Clients.Caller.broadcastActiveUsers(users.ToArray());
                AddNewUser(Context.ConnectionId, userName, platform);
            }

            Clients.Caller.broadcastHubMessage("Welcome to DIRC!");
        }

        private void AddNewUser(string connectionId, string userName, string platform)
        {
            Console.WriteLine("User {0} on {1} registered with connectionID '{2}'.", userName, platform, connectionId);
            var newUser = new DircUser { UserName = userName, Platform = platform, ConnectionId = connectionId };
            users.Add(newUser);
            Clients.Others.broadcastNewUser(newUser.ConnectionId, newUser.UserName, newUser.Platform);
        }

        public override Task OnConnected()
        {
            Console.WriteLine("Connection from {0}", Context.ConnectionId);
            Clients.Caller.broadcastMessage("DircHub", "GNU/Linux", "Welcome to DIRC!");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine("OnDisconnected {0}, from {1}", stopCalled, Context.ConnectionId);
            RemoveUser(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        private void RemoveUser(string connectionId)
        {
            if (users.Exists(u => u.ConnectionId == connectionId))
            {
                Clients.Others.broadcastUserLeft(connectionId);
                users.RemoveAll(u => u.ConnectionId == connectionId);
            }
        }

        public override Task OnReconnected()
        {
            Console.WriteLine("OnReconnected from {0}", Context.ConnectionId);
            return base.OnReconnected();
        }

        public class DircUser
        {
            public string UserName { get; set; }
            public string Platform { get; set; }
            public string ConnectionId { get; set; }
        }
    }
}