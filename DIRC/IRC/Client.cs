using System;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace DIRC.IRC
{
    public class Client
    {
		const string MyConnectionId = "MyConnectionId";

		public event EventHandler<string> OnMessageReceived;
		public event EventHandler<ICollection<DircUser>> OnConnectedToHub;
		public event EventHandler<DircUser> OnNewUser;
		public event EventHandler<string> OnUserLeft;

		readonly string userName;
        readonly string platform;
        readonly HubConnection connection;
        readonly IHubProxy hub;
		readonly IDictionary<string, DircUser> users;

        public Client(string userName)
        {
            this.userName = userName;
			users = new Dictionary<string, DircUser>();
            platform = Device.OS.ToString();
            connection = new HubConnection("http://mildestve.it:1337");
            hub = connection.CreateHubProxy("DircHub");
			hub.On("broadcastHubMessage", (string message) => {
				var messageReceived = OnMessageReceived;
				if (messageReceived != null)
				{
					messageReceived(this, string.Format("\ud83d\udcbb {0}", message));
				}
			});

			hub.On("broadcastDircMessage", (string connectionId, string message) =>
            {
                var messageReceived = OnMessageReceived;
                if (messageReceived != null)
                {
					var user = users[connectionId];
					messageReceived(this, string.Format("{0} {1} {2}", user.UserName, DircUser.GetPlatformText(user.Platform), message));
                }
            });

			hub.On("broadcastActiveUsers", (object[] currentUsers) =>
            {
				users.Clear();
				foreach (var json in currentUsers) {
					var user = JsonConvert.DeserializeObject<DircUser>(json.ToString());
					users.Add(user.ConnectionId, user);
				}
							
				if(!users.ContainsKey(MyConnectionId)) {
					users.Add(MyConnectionId, new DircUser { ConnectionId = MyConnectionId, UserName = userName, Platform = platform });
				}
                
				var connectedToHub = OnConnectedToHub;
                if (connectedToHub != null)
                {
					connectedToHub(this, users.Values);
                }
            });

            hub.On("broadcastNewUser", (string connectionId, string theUserName, string thePlatform) =>
            {
				if (users.ContainsKey(connectionId)) {
					users[connectionId] = new DircUser { ConnectionId = connectionId, UserName = theUserName, Platform = thePlatform };
				} else {
					users.Add(connectionId, new DircUser { ConnectionId = connectionId, UserName = theUserName, Platform = thePlatform });
				}
				
                var onNewUser = OnNewUser;
                if (onNewUser != null)
                {
                    onNewUser(this, new DircUser { ConnectionId = connectionId, UserName = theUserName, Platform = thePlatform });
                }
            });

            hub.On("broadcastUserLeft", (string connectionId) =>
            {
				if (users.ContainsKey(connectionId)) {
					users.Remove(connectionId);
				}

                var onUserLeft = OnUserLeft;
                if (onUserLeft != null)
                {
                    onUserLeft(this, connectionId);
                }
            });
        }

        public async Task Connect()
        {
            await connection.Start();
            await hub.Invoke("register", userName, platform);
        }

        public async Task Send(string message)
        {
            bool failed = false;
            try
            {
                await SendMessage(message);
            }
            catch (Exception)
            {
                failed = true;
            }

            if (failed)
            {
                await Connect();
                await SendMessage(message);
            }
        }

        async Task SendMessage(string message)
        {
			await hub.Invoke("SendDircMessage", message);
        }
    }

	public class DircUser {
		public string UserName { get; set; }
		public string Platform { get; set; }
		public string ConnectionId { get; set; }

		public static string GetPlatformText(string platform) {
			var targetPlatform = TargetPlatform.Other;
			Enum.TryParse<TargetPlatform>(platform, out targetPlatform);
			switch (Device.OS) {
				case TargetPlatform.iOS:
					switch (targetPlatform) {
						case TargetPlatform.iOS:
							return "";
						case TargetPlatform.Android:
							return "\ud83d\udc7d";
						case TargetPlatform.WinPhone:
							return "\ud83d\udeaa";
						default:
							switch (platform.ToLower()) {
								case "gnu/linux":
									return "\ud83d\udcbb";
							}

							break;
					}

					break;
				case TargetPlatform.Android:
					break;
				case TargetPlatform.WinPhone:
					break;
			}


			return "(" + platform + "):";
		}
	}

	//	platform_icons["default"] = '<i class="fa fa-question"></i>';
	//	platform_icons["android"] = '<i class="fa fa-android"></i>';
	//	platform_icons["ios"] = '<i class="fa fa-apple"></i>';
	//	platform_icons["winphone"] = '<i class="fa fa-windows"></i>';
	//	platform_icons["windows"] = '<i class="fa fa-windows"></i>';
	//	platform_icons["gnu/linux"] = '<i class="fa fa-linux"></i>';
	//	platform_icons["safari"] = '<i class="fa fa-compass"></i>';
	//	platform_icons["chrome"] = '<i class="icon-chrome"></i>';
	//	platform_icons["opera"] = '<i class="icon-opera"></i>';
	//	platform_icons["ie"] = '<i class="icon-ie"></i>';
	//	platform_icons["firefox"] = '<i class="icon-firefox"></i>';

}

