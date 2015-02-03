using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DIRC.ViewModels;

namespace DIRC.View {
	public partial class MessagesView : ContentPage {
		readonly MessagesViewModel vm;

		public MessagesView(string userName) {
			InitializeComponent();
			BindingContext = vm = new MessagesViewModel(Navigation, userName);
			NavigationPage.SetHasBackButton(this, false);
            Device.OnPlatform(() => input.BackgroundColor = new Color(248D, 248D, 248D));
			messages.HasUnevenRows = true;
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			await vm.Init();
		}

		void MessageSelected(object sender, EventArgs e) {
			messages.SelectedItem = null;
		}
	}
}

