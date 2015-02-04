using System;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace DIRC.IRC
{
	public class Client
	{
		readonly string userName;
		readonly string platform;
		readonly HubConnection connection;
		readonly IHubProxy hub;

		public event EventHandler<string> OnMessageReceived;
		public event EventHandler<IList<DircUser>> OnConnectedToHub;
		public event EventHandler<DircUser> OnNewUser;
		public event EventHandler<string> OnUserLeft;

		public Client(string userName)
		{
			this.userName = userName;
			platform = Device.OS.ToString();
			connection = new HubConnection("http://mildestve.it:1337");
			hub = connection.CreateHubProxy("DircHub");
			hub.On("broadcastMessage", (string theUserName, string thePlatform, string message) =>
				{
					var messageReceived = OnMessageReceived;
					if (messageReceived != null) {
						messageReceived(this, string.Format("{0} ({1}): {2}", theUserName, thePlatform, message));
					}
				});

			hub.On("broadcastActiveUsers", (IEnumerable<object> users) =>
				{
					var connectedToHub = OnConnectedToHub;
					if (connectedToHub != null) {
						connectedToHub(this, users.Cast<DircUser>().ToList());
					}
				});

			hub.On("broadcastNewUser", (string connectionId, string theUserName, string thePlatform) =>
				{
					var onNewUser = OnNewUser;
					if (onNewUser != null) {
						onNewUser(this, new DircUser { ConnectionId = connectionId, UserName = theUserName, Platform = thePlatform });
					}
				});

			hub.On("broadcastUserLeft", (string connectionId) =>
				{
					var onUserLeft = OnUserLeft;
					if (onUserLeft != null) {
						onUserLeft(this, connectionId);
					}
				});
		}

		public async Task Connect()
		{
			await connection.Start();
			// TODO: Funker sikkert med ny hub..?
			//await hub.Invoke("register", userName, platform);
		}

		public async Task Send(string message)
		{
			bool failed = false;
			try {
				await SendMessage(message);
			} catch (Exception)
			{
				failed = true;
			}

			if (failed)
			{
				await Connect();
				await SendMessage(message);
			}
		}

		async Task SendMessage(string message) {
			await hub.Invoke("send", userName, platform, message);
		}
	}
}

