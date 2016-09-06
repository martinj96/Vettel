using Xamarin.Forms;
using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections;
using Transpo.Models;
using System.Collections.Generic;

namespace Transpo.Mobile
{
	public partial class RidesPage : ContentPage
	{
		public ListView ListView { get { return lvRides; } }
		public RidesPage()
		{
			InitializeComponent();

			var client = new HttpClient();
			client.BaseAddress = new Uri("http://api.kinisaj.mk/");
			var response = client.GetAsync("search/all").Result;
			var json = response.Content.ReadAsStringAsync().Result;
			var obj = JsonConvert.DeserializeObject<SearchResultModel>(json);

			lvRides.ItemsSource = obj.Rides;
		}
	}
}

