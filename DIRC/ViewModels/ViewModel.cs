using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DIRC.Annotations;

namespace DIRC {
	public class ViewModel : INotifyPropertyChanged {
		public ViewModel() {
		}

	    public event PropertyChangedEventHandler PropertyChanged;

	    [NotifyPropertyChangedInvocator]
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

