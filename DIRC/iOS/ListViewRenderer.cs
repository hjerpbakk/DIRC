using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly:ExportRenderer(typeof(DIRC.CustomListView), typeof(DIRC.iOS.CustomListViewRenderer))]
namespace DIRC.iOS
{
	public class CustomListViewRenderer : ListViewRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);

			if (Control == null)
				return;

			var listView = Control as UITableView;
			listView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
		}
	}
}

