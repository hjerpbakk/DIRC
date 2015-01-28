using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DIRC.ViewModels {
	public class MessagesViewModel : INotifyPropertyChanged {
		readonly string title;

		public MessagesViewModel(string userName) {
			title = userName;
		}

		public string Title { get { return title; } }

		public event PropertyChangedEventHandler PropertyChanged;

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

