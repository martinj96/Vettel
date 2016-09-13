using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Transpo.Models;
using Xamarin.Auth;
using Xamarin.Forms;
using System.Text;
using System.Net.Http;

namespace Transpo.Mobile
{
	public partial class CreateRideDetailsPage : ContentPage
	{
		float fromLat, fromLon, toLat, toLon;
		string fromName, toName;

		public string UserInfo
		{
			get
			{
				var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
				return (account != null) ? account.Properties["Model"] : null;
			}
		}

		public string Password
		{
			get
			{
				var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
				return (account != null) ? account.Properties["Password"] : null;
			}
		}

		public string Username
		{
			get
			{
				var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
				return (account != null) ? account.Username : null;
			}
		}

		public CreateRideDetailsPage()
		{
			InitializeComponent();
		}

		public CreateRideDetailsPage(float fromLat, float fromLon, float toLat, float toLon, string fromName, string toName)
		{
			this.fromLat = fromLat;
			this.fromLon = fromLon;
			this.toLat = toLat;
			this.toLon = toLon;
			this.fromName = fromName;
			this.toName = toName;

			InitializeComponent();
			departureDate.SetValue(DatePicker.MinimumDateProperty, DateTime.Now.AddDays(1));
		}

		public void CreateRide(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(seatsLeft.Text))
			{
				lblError.Text = "Внесете слободни места";
				lblError.IsVisible = true;
				loader.IsRunning = false;
				return;
			}

			if (String.IsNullOrEmpty(price.Text))
			{
				lblError.Text = "Внесете цена";
				lblError.IsVisible = true;
				loader.IsRunning = false;
				return;
			}

			RideModel model = new RideModel();
			model.StartPoint = new Models.Point
			{
				Latitude = (decimal)fromLat,
				Longitude = (decimal)fromLon,
				Name = fromName
			};
			model.EndPoint = new Models.Point
			{
				Latitude = (decimal)toLat,
				Longitude = (decimal)toLon,
				Name = toName
			};
			model.DepartureDate = departureDate.Date;
			model.TimeDeparture = new Time
			{
				Hour = departureTime.Time.Hours,
				Minutes = departureTime.Time.Minutes
			};
			model.Description = description.Text;
			model.PricePerPassenger = Convert.ToInt32(price.Text);
			model.SeatsLeft = Convert.ToInt32(seatsLeft.Text);

			if (UserInfo == null)
			{
				return;
			}
			UserViewModel user = JsonConvert.DeserializeObject<UserViewModel>(UserInfo);
			model.Driver = user;
			model.Waypoints = new List<Models.Point>();

			var client = new HttpClient();
			client.BaseAddress = new Uri("http://api.kinisaj.mk/");
			var json = JsonConvert.SerializeObject(model);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			byte[] bArray = System.Text.Encoding.UTF8.GetBytes(Username + ":" + Password);
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bArray));
			var response = client.PostAsync("rides/create", content).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var jsonResult = response.Content.ReadAsStringAsync().Result;
				MessagingCenter.Send<ContentPage>(this, "Refresh");
				MessagingCenter.Send<ContentPage>(this, "NavigateToRides");
			}
			else
			{
				lblError.Text = "Внесовте невалидни податоци";
				lblError.IsVisible = true;
				loader.IsRunning = false;
			}
		}
	}
}

