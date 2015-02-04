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
							return "";
						case TargetPlatform.WinPhone:
							return "U+1F38F";
						default:
							switch (platform.ToLower()) {
								case "safari":
									return "";
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
}

