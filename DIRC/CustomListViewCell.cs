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
	}
}

