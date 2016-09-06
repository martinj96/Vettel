using System;
using Foundation;
using UIKit;

public class RideNativeiOSCell : UITableViewCell
{
	UILabel startPoint, endPoint, seats, price, departure;

	public RideNativeiOSCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
	{
		startPoint = new UILabel() {
			Font = UIFont.FromName("Helvetica Neue", 13f)
		};

		endPoint = new UILabel() {
			Font = UIFont.FromName("Helvetica Neue", 13f),
			TextAlignment = UITextAlignment.Right
		};

		seats = new UILabel() {
			Font = UIFont.FromName("Helvetica Neue", 12f),
			TextAlignment = UITextAlignment.Center,
			Alpha = 0.8f
		};

		price = new UILabel() {
			Font = UIFont.FromName("Helvetica Neue", 12f),
			TextAlignment = UITextAlignment.Right,
			Alpha = 0.8f
		};

		departure = new UILabel()
		{
			Font = UIFont.FromName("Helvetica Neue", 12f),
			Alpha = 0.8f
		};

		ContentView.Add(startPoint);
		ContentView.Add(endPoint);
		ContentView.Add(seats);
		ContentView.Add(price);
		ContentView.Add(departure);
	}

	public void UpdateCell(string startPoint, string endPoint, string seats, string price, string departure)
	{
		this.startPoint.Text = startPoint;
		this.endPoint.Text = endPoint;
		this.seats.Text = seats + " места";
		this.price.Text = price + " MKD";
		this.departure.Text = departure;
	}

	public override void LayoutSubviews()
	{
		base.LayoutSubviews();

		startPoint.Frame = new CoreGraphics.CGRect(5, 
		                                           1, 
		                                           ContentView.Bounds.Width * 4 / 10, 
		                                           ContentView.Bounds.Height * 2 / 3 - 5);

		endPoint.Frame = new CoreGraphics.CGRect(ContentView.Bounds.Width * 6 / 10,
		                                         1, 
		                                         ContentView.Bounds.Width * 4 / 10 - 10,
		                                         ContentView.Bounds.Height * 2 / 3 - 5);

		departure.Frame = new CoreGraphics.CGRect(5, 
		                                          ContentView.Bounds.Height * 2 / 3 - 2, 
		                                          ContentView.Bounds.Width * 2 / 3, 
		                                          ContentView.Bounds.Height / 4);

		seats.Frame = new CoreGraphics.CGRect(5 + ContentView.Bounds.Width / 3,
												  ContentView.Bounds.Height * 2 / 3 - 2,
												  ContentView.Bounds.Width / 3 - 5,
												  ContentView.Bounds.Height / 4);

		price.Frame = new CoreGraphics.CGRect(5 + ContentView.Bounds.Width * 2 / 3,
												  ContentView.Bounds.Height * 2 / 3 - 2,
												  ContentView.Bounds.Width / 3 - 10,
												  ContentView.Bounds.Height / 4);
	}
}