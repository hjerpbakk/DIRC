using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Foundation;
using UIKit;

[assembly:ExportRenderer(typeof(DIRC.CustomListViewCell), typeof(DIRC.iOS.LolRenderer))]

namespace DIRC.iOS
{
	public class LolRenderer : ViewCellRenderer
	{
		const string Id = "lol";

		public override UIKit.UITableViewCell GetCell (Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tableView)
		{

			var thecell = (CustomListViewCell)item;
			var cell = (BubbleCell) tableView.DequeueReusableCell (Id) ?? new BubbleCell (thecell.IsMe);
			cell.Update (thecell.Text);
			return cell;

		}
	}
}

