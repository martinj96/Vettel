using System;
using Foundation;
using Transpo.Mobile;
using Transpo.Mobile.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RideNativeCell), typeof(RideNativeiOSCellRenderer))]
namespace Transpo.Mobile.iOS
{
	public class RideNativeiOSCellRenderer : ViewCellRenderer
	{
		static NSString rid = new NSString("RideNativeCell");

		public override UITableViewCell GetCell(Xamarin.Forms.Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var x = (RideNativeCell)item;
			Console.WriteLine(x);

			RideNativeiOSCell c = reusableCell as RideNativeiOSCell;

			if (c == null)
			{
				c = new RideNativeiOSCell(rid);
			}

			c.UpdateCell(x.StartPoint, x.EndPoint, x.Seats, x.Price, x.Departure);

			return c;
		}
	}
}
