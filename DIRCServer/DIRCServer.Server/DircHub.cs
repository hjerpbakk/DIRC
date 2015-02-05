using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace DIRCServer.Server
{
    public class DircHub : Hub
    {
        private const int MaxUserNameLength = 20;

        private static List<DircUser> users = new List<DircUser>();

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

        public override Task OnReconnected()
        {
            Console.WriteLine("OnReconnected from {0}", Context.ConnectionId);
            return base.OnReconnected();
        }

        public void SendDircMessage(string message)
        {
            message = CleanInput(message);

            if (!users.Exists(u => u.ConnectionId == Context.ConnectionId))
            {
                Console.WriteLine("Unauthorized user ({0}) attempted to send '{1}' to others.", Context.ConnectionId, message);
                return;
            }

            Console.WriteLine("Broadcast message \"{0}\" from {1} to others.", message, Context.ConnectionId);

            // backwards compatibility
            try
            {
                var userName = users.Find(u => u.ConnectionId == Context.ConnectionId).UserName;
                var platform = users.Find(u => u.ConnectionId == Context.ConnectionId).Platform;
                Clients.Others.broadcastMessage(userName, platform, message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.StackTrace);
            }

            Clients.Others.broadcastDircMessage(Context.ConnectionId, message);
        }

        public void Register(string userName, string platform)
        {
            userName = CleanUserName(userName);
            platform = CleanInput(platform);

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

        private void RemoveUser(string connectionId)
        {
            if (users.Exists(u => u.ConnectionId == connectionId))
            {
                Clients.Others.broadcastUserLeft(connectionId);
                users.RemoveAll(u => u.ConnectionId == connectionId);
            }
        }

        [Obsolete("Send is deprecated, please use Register and SendDircMessage instead.")]
        public void Send(string userName, string platform, string message)
        {
            userName = CleanUserName(userName);
            message = CleanInput(message);
            platform = CleanInput(platform);

            if (!users.Exists(u => u.ConnectionId == Context.ConnectionId))
            {
                AddNewUser(Context.ConnectionId, userName, platform);
            }

            Console.WriteLine("Broadcast message \"{0}\" from {1} to others.", message, Context.ConnectionId);
            Clients.Others.broadcastMessage(userName, platform, message);
            Clients.Others.broadcastDircMessage(Context.ConnectionId, message);
        }

        private static string CleanInput(string input)
        {
            //return Regex.Replace(input, @"[^\w\s\-\+]", string.Empty);
            return HttpUtility.HtmlEncode(input);
        }

        private static string CleanUserName(string userName)
        {
            userName = CleanInput(userName);
            userName = userName.Length > MaxUserNameLength ? userName.Substring(0, MaxUserNameLength) : userName;
            return userName;
        }
    }
}