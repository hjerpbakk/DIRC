using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using DIRC.View;

namespace DIRC {
	public class LogonViewModel : INotifyPropertyChanged {
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

	    public event PropertyChangedEventHandler PropertyChanged;

		async void Logon() {
			await navigation.PushAsync(new MessagesView(userName));
		}

	    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
	    {
	        var handler = PropertyChanged;
	        if (handler != null)
	        {
	            handler(this, new PropertyChangedEventArgs(propertyName));
	        }
	    }
	}
}

