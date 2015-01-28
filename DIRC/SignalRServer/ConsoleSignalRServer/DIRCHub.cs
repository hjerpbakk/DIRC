using Microsoft.AspNet.SignalR;

namespace ConsoleSignalRServer
{
    public class DIRCHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void SendDIRCMessage(string message)
        {
            Clients.All.broadcastDIRCMessage(message);
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }
}