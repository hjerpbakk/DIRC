using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using DIRC.IRC;
using System.Threading.Tasks;
using DIRC.View;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;

namespace DIRC.ViewModels {
	public class MessagesViewModel : ViewModelBase {
		readonly string userName;
		readonly Command sendCommand;
		readonly ObservableCollection<DIRCMessage> messages;
		readonly Client client;
		readonly INavigation navigation;

		string message;
		ObservableCollection<DircUser> users;

		public MessagesViewModel(INavigation navigation, string userName) {
			this.navigation = navigation;
			this.userName = userName;
			client = new Client(userName);
			sendCommand = new Command(() => Send(), () => !string.IsNullOrEmpty(message));
			messages = new ObservableCollection<DIRCMessage>();
		}

		public string Title { get { return userName; } }

		public ObservableCollection<DIRCMessage> Messages { get { return messages; } }
		public ObservableCollection<DircUser> Users { get { return users; } set { users = value; OnPropertyChanged(); }}

		public string Message {
			get { return message; }
			set {
				message = value;
				OnPropertyChanged();
				sendCommand.ChangeCanExecute();
			}
		}

		public Command SendCommand { get { return sendCommand; } }

		public async Task Init() {
			try {
				client.OnMessageReceived += HandleOnMessageReceived;
				client.OnConnectedToHub += (sender, theUsers) => {
					Users = new ObservableCollection<DircUser>(theUsers);
					Messenger.Default.Send(Users.Count);
				};
				client.OnNewUser += (sender, user) => {
					Users.Add(user);
					Messenger.Default.Send(Users.Count);
				};
				client.OnUserLeft += (sender, id) => {
					var user = Users.SingleOrDefault(u => u.ConnectionId == id);
					if (user != null) {
						Users.Remove(user);
						Messenger.Default.Send(Users.Count);
					}
				};
				await client.Connect();
				message = "Connected";
				await Send();
			} catch (Exception ex) {
				ShowMessage(new DIRCMessage{ Text = "!Init!: " + ex.Message, FromOthers = false });
			}
		}

		async Task Send() {
			try {
				ShowMessage(new DIRCMessage{ Text = message, FromOthers = false });
				var theMessage = message;
				Message = "";
				await client.Send(theMessage);	
			} catch (Exception ex) {
				ShowMessage(new DIRCMessage{ Text = "!Send!: " + ex.Message, FromOthers = false });
			}
		}

		void ShowMessage(DIRCMessage theMessage) {
			messages.Add(theMessage);
		}

		void HandleOnMessageReceived(object sender, string theMessage) {
			ShowMessage(new DIRCMessage{ Text = theMessage, FromOthers = true });
		}
	}

	public class DIRCMessage {
		public string Text{ get; set; }
		public bool FromOthers{ get; set; }
	}
}

