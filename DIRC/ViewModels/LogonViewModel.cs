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

		bool m_logonEnabled;


		public LogonViewModel(INavigation navigation) {
			this.navigation = navigation;
			logonCommand = new Command(Logon);
		}


		public bool longonEnabled{
			get { return m_logonEnabled; }
			set{
				m_logonEnabled = value;
				OnPropertyChanged();
			}


		}

		public string UserName {
			get { return userName; }
			set {
				userName = value;

				longonEnabled = !String.IsNullOrEmpty (UserName);
				OnPropertyChanged();
			}
		}

		public Command LogonCommand { get { return logonCommand; } }

		async void Logon() {
			if (!String.IsNullOrEmpty(UserName)) {
				await navigation.PushAsync (new MessagesView (userName));
			}
		}
	}
}

