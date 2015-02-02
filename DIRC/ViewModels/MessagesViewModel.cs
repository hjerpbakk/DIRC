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
		readonly ObservableCollection<string> messages;
		readonly Client client;
		readonly INavigation navigation;

		string message;
		string selectedMessage;

		public MessagesViewModel(INavigation navigation, string userName) {
			this.navigation = navigation;
			this.userName = userName;
			client = new Client();
			sendCommand = new Command(() => Send(), () => !string.IsNullOrEmpty(message));
			messages = new ObservableCollection<string>();
		}

		public string Title { get { return userName; } }

		public ObservableCollection<string> Messages { get { return messages; } }

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
				ShowMessage("!Init!: " + ex.Message);
			}
		}

		async Task Send() {
			try {
				ShowMessage(message);
				await client.Send(userName, message);	
			} catch (Exception ex) {
				ShowMessage("!Send!: " + ex.Message);
			} finally {
				Message = "";
			}
		}

		void ShowMessage(string theMessage) {
			messages.Add(theMessage);
		}

		void HandleOnMessageReceived (object sender, string theMessage)
		{
			ShowMessage(theMessage);
		}
	}
}

