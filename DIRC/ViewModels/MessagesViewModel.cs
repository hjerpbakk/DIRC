using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using DIRC.IRC;
using System.Threading.Tasks;
using DIRC.View;

namespace DIRC.ViewModels {
	public class MessagesViewModel : ViewModelBase {
		readonly string userName;
		readonly Command sendCommand;
		readonly ObservableCollection<DIRCMessage> messages;
		readonly Client client;
		readonly INavigation navigation;
		string message;

		public MessagesViewModel(INavigation navigation, string userName) {
			this.navigation = navigation;
			this.userName = userName;
			client = new Client();
			sendCommand = new Command(() => Send(), () => !string.IsNullOrEmpty(message));
			messages = new ObservableCollection<DIRCMessage>();
		}

		public string Title { get { return userName; } }

		public ObservableCollection<DIRCMessage> Messages { get { return messages; } }

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
				await client.Connect();
				message = "Connected";
				await Send();
			} catch (Exception ex) {
				ShowMessage(new DIRCMessage{Text="!Init!: " + ex.Message, IsItMe=false});
			}
		}

		async Task Send() {
			try {
				ShowMessage(new DIRCMessage{Text=message, IsItMe=false});
				await client.Send(userName, message);	
			} catch (Exception ex) {
				ShowMessage(new DIRCMessage{Text="!Send!: " + ex.Message, IsItMe=false});
			} finally {
				Message = "";
			}
		}

		void ShowMessage(DIRCMessage theMessage) {
			messages.Add(theMessage);
		}

		void HandleOnMessageReceived (object sender, string theMessage)
		{
			ShowMessage(new DIRCMessage{Text=theMessage, IsItMe=true});
		}
	}
	public class DIRCMessage{
		public string Text{ get; set; }
		public bool IsItMe{ get; set; }
	}
}

