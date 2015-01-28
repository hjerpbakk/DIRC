using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DIRC.View {
	public partial class MessageView : ContentPage {
		public MessageView(string message) {
			InitializeComponent();
			Message.Text = message;
		}
	}
}

