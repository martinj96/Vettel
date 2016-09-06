using System;
using Xamarin.Forms;

namespace Transpo.Mobile
{
	public class RideNativeCell : ViewCell
	{
		public static readonly BindableProperty StartPointProperty = 
			BindableProperty.Create("StartPoint", typeof(string), typeof(RideNativeCell), "");

		public string StartPoint
		{
			get { return (string)GetValue(StartPointProperty); }
			set { SetValue(StartPointProperty, value); }
		}

		public static readonly BindableProperty EndPointProperty =
			BindableProperty.Create("EndPoint", typeof(string), typeof(RideNativeCell), "");

		public string EndPoint
		{
			get { return (string)GetValue(EndPointProperty); }
			set { SetValue(EndPointProperty, value); }
		}

		public static readonly BindableProperty SeatsProperty =
			BindableProperty.Create("Seats", typeof(string), typeof(RideNativeCell), "");

		public string Seats
		{
			get { return (string)GetValue(SeatsProperty); }
			set { SetValue(SeatsProperty, value); }
		}

		public static readonly BindableProperty PriceProperty =
  			BindableProperty.Create("Price", typeof(string), typeof(RideNativeCell), "");

		public string Price
		{
			get { return (string)GetValue(PriceProperty); }
			set { SetValue(PriceProperty, value); }
		}

		public static readonly BindableProperty DepartureProperty =
  			BindableProperty.Create("Departure", typeof(string), typeof(RideNativeCell), "");

		public string Departure
		{
			get { return (string)GetValue(DepartureProperty); }
			set { SetValue(DepartureProperty, value); }
		}
	}
}

