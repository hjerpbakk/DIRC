using Xamarin.Forms;
using System;

namespace DIRC.IRC {
	public class DircUser {
		public string UserName { get; set; }
		public string Platform { get; set; }
		public string ConnectionId { get; set; }

		public static string GetPlatformText(string platform) {
			var targetPlatform = TargetPlatform.Other;
			Enum.TryParse<TargetPlatform>(platform, out targetPlatform);
			switch (Device.OS) {
				case TargetPlatform.iOS:
					switch (targetPlatform) {
						case TargetPlatform.iOS:
							return "";
						case TargetPlatform.Android:
							return "\ud83d\udc7d";
						case TargetPlatform.WinPhone:
							return "\ud83d\udeaa";
						default:
							switch (platform.ToLower()) {
								case "gnu/linux":
									return "\ud83d\udcbb";
							}

							break;
					}

					break;
				case TargetPlatform.Android:
					break;
				case TargetPlatform.WinPhone:
					break;
			}


			return "(" + platform + "):";
		}
	}

//	platform_icons["default"] = '<i class="fa fa-question"></i>';
//	platform_icons["android"] = '<i class="fa fa-android"></i>';
//	platform_icons["ios"] = '<i class="fa fa-apple"></i>';
//	platform_icons["winphone"] = '<i class="fa fa-windows"></i>';
//	platform_icons["windows"] = '<i class="fa fa-windows"></i>';
//	platform_icons["gnu/linux"] = '<i class="fa fa-linux"></i>';
//	platform_icons["safari"] = '<i class="fa fa-compass"></i>';
//	platform_icons["chrome"] = '<i class="icon-chrome"></i>';
//	platform_icons["opera"] = '<i class="icon-opera"></i>';
//	platform_icons["ie"] = '<i class="icon-ie"></i>';
//	platform_icons["firefox"] = '<i class="icon-firefox"></i>';
}

