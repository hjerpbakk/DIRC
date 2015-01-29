using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace DIRC.iOS {
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {
		const int NavigationBarTintColor = 0xf8f8f8;

		public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App());

			UINavigationBar.Appearance.BarTintColor = FromHex(NavigationBarTintColor);

			return base.FinishedLaunching(app, options);
		}

		private static UIColor FromHex(int hexValue)
		{
			return UIColor.FromRGB(
				(((float)((hexValue & 0xFF0000) >> 16))/255.0f),
				(((float)((hexValue & 0xFF00) >> 8))/255.0f),
				(((float)(hexValue & 0xFF))/255.0f)
			);
		}
	}
}

