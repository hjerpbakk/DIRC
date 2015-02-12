using System;
using DIRC.iOS.View;
using DIRC.View;
using Xamarin.Forms;
using Foundation;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(MessagesView), typeof(IosPageRenderer))]


namespace DIRC.iOS.View {
	public class IosPageRenderer : PageRenderer {
		NSObject observerHideKeyboard;
		NSObject observerShowKeyboard;

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			observerHideKeyboard = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
			observerShowKeyboard = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			NSNotificationCenter.DefaultCenter.RemoveObserver(observerHideKeyboard);
			NSNotificationCenter.DefaultCenter.RemoveObserver(observerShowKeyboard);
		}

		void OnKeyboardNotification(NSNotification notification)
		{
			if (!IsViewLoaded) return;

			var frameBegin = UIKeyboard.FrameBeginFromNotification(notification);
			var frameEnd = UIKeyboard.FrameEndFromNotification(notification);
			var bounds = Element.Bounds;
			var newBounds = new Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height - frameBegin.Top + frameEnd.Top);
			Element.Layout(newBounds);
		}
	}
}

