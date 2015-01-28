using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using DIRC.View;
using DIRC.ViewModels;

namespace DIRC {
	public class LogonViewModel : ViewModelBase {
		readonly Command logonCommand;
		readonly INavigation navigation;

		string userName;

		public LogonViewModel(INavigation navigation) {
			this.navigation = navigation;
			logonCommand = new Command(Logon);
		}

		public string UserName {
			get { return userName; }
			set {
				userName = value;
				OnPropertyChanged();
			}
		}

		public Command LogonCommand { get { return logonCommand; } }

		async void Logon() {
			await navigation.PushAsync(new MessagesView(userName));
		}
	}
}

