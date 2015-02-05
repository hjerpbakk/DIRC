using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DIRC.ViewModels;
using GalaSoft.MvvmLight.Messaging;

namespace DIRC.View {
	public partial class MessagesView : ContentPage {
		readonly MessagesViewModel vm;

		bool loaded;
		ToolbarItem usersItem;

		public MessagesView(string userName) {
			InitializeComponent();
			BindingContext = vm = new MessagesViewModel(Navigation, userName);
			NavigationPage.SetHasBackButton(this, false);
            Device.OnPlatform(() => input.BackgroundColor = new Color(248D, 248D, 248D));
			messages.HasUnevenRows = true;

			usersItem = new ToolbarItem("Users", null, 	
				() => { 
					var userList = new UserListView();
					userList.BindingContext = BindingContext;
					Navigation.PushAsync(userList);	 
				}
			);
			ToolbarItems.Add(usersItem);
			Messenger.Default.Register<int>(this, n => {
				usersItem.Text = n + " Users";
				ToolbarItems.Clear();
				ToolbarItems.Add(usersItem);
			});
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			if (loaded) {
				return;
			}

			loaded = true;
			await vm.Init();
		}

		void MessageSelected(object sender, EventArgs e) {
			messages.SelectedItem = null;
		}
	}
}

