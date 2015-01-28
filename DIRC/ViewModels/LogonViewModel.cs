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

	    public event PropertyChangedEventHandler PropertyChanged;

		async void Logon() {
			await navigation.PushAsync(new MessagesView());
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

