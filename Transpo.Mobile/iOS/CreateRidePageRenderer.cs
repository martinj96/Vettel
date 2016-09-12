using System;
using CoreGraphics;
using DurianCode.iOS.Places;
using DurianCode.iOS.Places.GooglePlace;
using Newtonsoft.Json.Linq;
using Transpo.Mobile;
using Transpo.Mobile.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CreateRidePage), typeof(CreateRidePageRenderer))]
namespace Transpo.Mobile.iOS
{
	public class CreateRidePageRenderer : PageRenderer
	{
		public UIButton fromLocationButton;
		public UIButton toLocationButton;
		public UILabel fromLabel;
		public UILabel toLabel;
		public UIButton searchButton;
		public UINavigationController placesViewContainer;
		public PlacesViewController placesViewController;
		public nfloat fromLat, fromLon, toLat, toLon;
		public string fromName, toName;
		public int selected;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.White;

			selected = 0;
			nfloat h = 31.0f;
			nfloat w = View.Bounds.Width;

			fromLocationButton = new UIButton();
			fromLocationButton.TranslatesAutoresizingMaskIntoConstraints = false;
			fromLocationButton.Frame = new CGRect(10, 10, w - 20, h);
			fromLocationButton.SetTitle("Одбери место на поаѓање", UIControlState.Normal);
			fromLocationButton.SetTitleColor(UIColor.Blue, UIControlState.Normal);

			fromLocationButton.TouchUpInside += (sender, e) =>
			{
				// 1. Instantiate the PlacesViewController
				placesViewController = new PlacesViewController();
				placesViewController.apiKey = "AIzaSyDWag8hplmavNY64vFn_jPaUU9gl4AKGV4";

				// 2. (OPTIONAL) Set the search criteria to match your needs
				//placesViewController.SetPlaceType(PlaceType.Cities);
				//placesViewController.SetLocationBias(new LocationBias(40.7058316, -74.2581935, 1000000));

				// 3. Subscribe to PlaceSelected delegate to get place details
				placesViewController.PlaceSelected += HandleFromPlaceSelection;

				// 4. Instantiate the UINavigationController to contain the PlacesViewController
				placesViewContainer = new UINavigationController(placesViewController);

				// Optional: Customize the view styling
				//placesViewController.NavigationController.NavigationBar.BarStyle = UIBarStyle.Default;
				//placesViewController.NavigationController.NavigationBar.Translucent = false;
				//placesViewController.NavigationController.NavigationBar.TintColor = UIColor.Magenta;
				//placesViewController.NavigationController.NavigationBar.BarTintColor = UIColor.Yellow;
				//placesViewController.Title = "Type Address";

				// 5. Present the view
				PresentViewController(placesViewContainer, true, null);
			};

			toLocationButton = new UIButton();
			toLocationButton.TranslatesAutoresizingMaskIntoConstraints = false;
			toLocationButton.Frame = new CGRect(10, 45, w - 20, h);
			toLocationButton.SetTitle("Одбери место на пристигнување", UIControlState.Normal);
			toLocationButton.SetTitleColor(UIColor.Blue, UIControlState.Normal);

			toLocationButton.TouchUpInside += (sender, e) =>
			{
				// 1. Instantiate the PlacesViewController
				placesViewController = new PlacesViewController();
				placesViewController.apiKey = "AIzaSyDWag8hplmavNY64vFn_jPaUU9gl4AKGV4";

				// 2. (OPTIONAL) Set the search criteria to match your needs
				placesViewController.SetPlaceType(PlaceType.Cities);
				//placesViewController.SetLocationBias(new LocationBias(40.7058316, -74.2581935, 1000000));

				// 3. Subscribe to PlaceSelected delegate to get place details
				placesViewController.PlaceSelected += HandleToPlaceSelection;

				// 4. Instantiate the UINavigationController to contain the PlacesViewController
				placesViewContainer = new UINavigationController(placesViewController);

				// Optional: Customize the view styling
				//placesViewController.NavigationController.NavigationBar.BarStyle = UIBarStyle.Default;
				//placesViewController.NavigationController.NavigationBar.Translucent = false;
				//placesViewController.NavigationController.NavigationBar.TintColor = UIColor.Magenta;
				//placesViewController.NavigationController.NavigationBar.BarTintColor = UIColor.Yellow;
				//placesViewController.Title = "Type Address";

				// 5. Present the view
				PresentViewController(placesViewContainer, true, null);
			};

			fromLabel = new UILabel();
			fromLabel.Frame = new CGRect(10, 75, w - 20, h);
			fromLabel.Text = "Од: <неодбрано>";
			fromLabel.TextAlignment = UITextAlignment.Center;

			toLabel = new UILabel();
			toLabel.Frame = new CGRect(10, 110, w - 20, h);
			toLabel.Text = "До: <неодбрано>";
			toLabel.TextAlignment = UITextAlignment.Center;

			searchButton = new UIButton();
			searchButton.TranslatesAutoresizingMaskIntoConstraints = false;
			searchButton.Frame = new CGRect(10, 145, w - 20, h);
			searchButton.SetTitle("Внеси информации", UIControlState.Normal);
			searchButton.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			searchButton.Alpha = 0.0f;

			searchButton.TouchUpInside += (sender, e) =>
			{
				((Page)Element).Navigation.PushAsync(new CreateRideDetailsPage((float)fromLat, (float)fromLon, (float)toLat, (float)toLon, fromName, toName));
			};

			View.AddSubviews(new UIView[]
			{
				fromLocationButton,
				toLocationButton,
				fromLabel,
				toLabel,
				searchButton
			});
		}

		void HandleFromPlaceSelection(object sender, JObject placeData)
		{
			// 6. Handle the place details however you wish
			Console.WriteLine($"{placeData}");

			// You can utilize the Place object by importing 'DurianCode.iOS.Places.GooglePlace'
			var place = new Place(placeData);
			Console.WriteLine($"Place: {place.name}, Coordinates: {place.latitude},{place.longitude}");
			Console.WriteLine(place.raw); // prints the full place details json result
			fromLabel.Text = String.Format("Од: {0}", place.name);
			fromLat = Convert.ToSingle(place.latitude);
			fromLon = Convert.ToSingle(place.longitude);
			fromName = place.name;
			selected++;
			if (selected > 1)
				searchButton.Alpha = 1.0f;
		}

		void HandleToPlaceSelection(object sender, JObject placeData)
		{
			// 6. Handle the place details however you wish
			Console.WriteLine($"{placeData}");

			// You can utilize the Place object by importing 'DurianCode.iOS.Places.GooglePlace'
			var place = new Place(placeData);
			Console.WriteLine($"Place: {place.name}, Coordinates: {place.latitude},{place.longitude}");
			Console.WriteLine(place.raw); // prints the full place details json result
			toLabel.Text = String.Format("До: {0}", place.name);
			toLat = Convert.ToSingle(place.latitude);
			toLon = Convert.ToSingle(place.longitude);
			toName = place.name;
			selected++;
			if (selected > 1)
				searchButton.Alpha = 1.0f;
		}
	}
}

