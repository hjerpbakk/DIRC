using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using DIRC.IRC;
using System.Threading.Tasks;

namespace DIRC.ViewModels {
	public class MessagesViewModel : ViewModelBase {
		readonly string userName;
		readonly Command sendCommand;
		readonly ObservableCollection<string> messages;
		readonly Client client;

		string message;

		public MessagesViewModel(string userName) {
			this.userName = userName;
			client = new Client();
			sendCommand = new Command(() => Send());
			messages = new ObservableCollection<string>();
		}

		public string Title { get { return userName; } }

		public ObservableCollection<string> Messages { get { return messages; } }

		public string Message {
			get { return message; }
			set {
				message = value;
				OnPropertyChanged();
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
				ShowMessage("!Init!: " + ex.Message);
			}
		}

		// TODO: ?!?!?!?!?!??! TO feilmeldinger...
		async Task Send() {
			try {
				ShowMessage(message);
				await client.Send(userName, "message");		
			} catch (Exception ex) {
				ShowMessage("!Send!: " + ex.Message);
			}
		}

		void ShowMessage(string theMessage) {
			messages.Insert(0, theMessage);
		}

		void HandleOnMessageReceived (object sender, string theMessage)
		{
			ShowMessage(theMessage);
		}
	}
}

