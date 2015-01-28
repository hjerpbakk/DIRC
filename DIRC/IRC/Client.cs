using System;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DIRC.IRC {
	public class Client
	{
		readonly string _platform;
		readonly HubConnection _connection;
		readonly IHubProxy _proxy;

		public event EventHandler<string> OnMessageReceived;

		public Client()
		{
			_platform = Device.OS.ToString();
			_connection = new HubConnection("http://192.168.1.103");
			_proxy = _connection.CreateHubProxy("Chat");
		}

		public async Task Connect()
		{
			await _connection.Start();

			_proxy.On("messageReceived", (string platform, string message) =>
				{
					if (OnMessageReceived != null)
						OnMessageReceived(this, message);
				});
		}

		public Task Send(string userName, string message)
		{
			return _proxy.Invoke("Send", string.Format("{0} on {1}: {2}", userName, _platform, message));
		}
	}
}

