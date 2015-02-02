using System;
using Xamarin.Forms;

namespace DIRC
{
	public class RoundedBoxView : BoxView
	{
		public static readonly BindableProperty CornerRadiusProperty =
			BindableProperty.Create("CornerRadius", typeof(double), typeof(RoundedBoxView), 0.0);

		/// <summary>
		/// Gets or sets the corner radius.
		/// </summary>
		public double CornerRadius
		{
			get { return (double)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}
	}
}

