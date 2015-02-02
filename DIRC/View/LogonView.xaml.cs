using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DIRC.View {
	public partial class LogonView : ContentPage {
		public LogonView() {
			InitializeComponent();
			BindingContext = new LogonViewModel(Navigation);
			NavigationPage.SetHasNavigationBar(this, false);
			NavigationPage.SetHasBackButton(this, false);
		}

		protected override void OnDisappearing() {
			base.OnDisappearing();
			#if SILVERLIGHT
				Navigation.RemovePage(this);
			#endif 

			#if __IOS__
				Navigation.RemovePage(this);
			#endif
		}
	}
}

