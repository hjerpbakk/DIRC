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
		bool m_longonDisabled;

		public LogonViewModel(INavigation navigation) {
			this.navigation = navigation;
			logonCommand = new Command(Logon);
		}

		public bool logonEnabled{
			get{ return m_longonDisabled; }
			set {
				m_longonDisabled = value;
				OnPropertyChanged ();
			}
		}

		public string UserName {
			get { return userName; }
			set {
				userName = value;
				logonEnabled = !String.IsNullOrEmpty (userName);
				OnPropertyChanged();
			}
		}

		public Command LogonCommand { get { return logonCommand; } }

	    public event PropertyChangedEventHandler PropertyChanged;

		async void Logon() {
			if (!String.IsNullOrEmpty (UserName)) {
				await navigation.PushAsync (new MessagesView (userName));
			} else {

			}
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

