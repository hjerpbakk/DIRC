using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DIRC.ViewModels;

namespace DIRC.View {
	public partial class MessagesView : ContentPage {
		readonly MessagesViewModel vm;

		public MessagesView(string userName) {
			InitializeComponent();
			BindingContext = new MessagesViewModel(userName);
			NavigationPage.SetHasBackButton (this, false);
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			await vm.Init();
		}
	}
}

