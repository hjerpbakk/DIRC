using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DIRC.ViewModels;

namespace DIRC.View {
	public partial class MessagesView : ContentPage {
		public MessagesView(string userName) {
			InitializeComponent();
			BindingContext = new MessagesViewModel(userName);
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

		}
	}
}

