using System;
using Xamarin.Forms;

namespace DIRC
{
	public class CustomListViewCell : ViewCell {

		public static readonly BindableProperty IsMeProperty = BindableProperty.Create<CustomListViewCell, bool>(p => p.IsMe, false);
		
		public bool IsMe
		{
				get{ return (bool)GetValue (IsMeProperty);}
				set{ SetValue (IsMeProperty, value); }
		}

		public static readonly BindableProperty TextProperty = BindableProperty.Create<CustomListViewCell, string>(p => p.Text, "");

		public string Text
		{
			get{ return (string)GetValue (TextProperty);}
			set{ SetValue (IsMeProperty, value); }
		}

		const int avgCharsInRow = 10;
		const int defaultHeight = 44;
		const int extraLineHeight = 20;
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			if (Device.OS == TargetPlatform.iOS) { // don't bother on the other platforms
				var text = ((DIRC.ViewModels.DIRCMessage)BindingContext).Text;
				var len = text.Length;
				if (len < (avgCharsInRow * 2)) {
					// fits in one cell
					Height = defaultHeight;
				} else {
					len = len - (avgCharsInRow * 2);
					var extraRows = len / 35;
					Height = defaultHeight + extraRows * extraLineHeight;
				}
			}
		}
	}
}

