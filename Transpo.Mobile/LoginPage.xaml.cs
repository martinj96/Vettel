using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Transpo.Models;
using Xamarin.Auth;
using Xamarin.Forms;

namespace Transpo.Mobile
{
	public partial class LoginPage : ContentPage
	{
		public string Email
		{
			get
			{
				return email.Text;
			}
		}

		public string Password
		{
			get
			{
				return password.Text;
			}
		}

		public string UserName
		{
			get
			{
				var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
				return (account != null) ? account.Username : null;
			}
		}

		public LoginPage()
		{
			InitializeComponent();
		}

		public void OnLoginSubmit(object sender, EventArgs e)
		{
			loader.IsRunning = true;
			var client = new HttpClient();
			client.BaseAddress = new Uri("http://api.kinisaj.mk/");
			byte[] bArray = System.Text.Encoding.UTF8.GetBytes(Email + ":" + Password);
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bArray));
			var response = client.GetAsync("account/getuserinfo").Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var json = response.Content.ReadAsStringAsync().Result;
				if (string.IsNullOrEmpty(UserName))
				{
					Account account = new Account
					{
						Username = Email
					};
					account.Properties.Add("Password", Password);
					account.Properties.Add("Model", json);
					AccountStore.Create().Save(account, App.AppName);

					MessagingCenter.Send<ContentPage>(this, "Refresh");
					MessagingCenter.Send<ContentPage>(this, "NavigateToRides");
				}
			}
			else
			{
				lblError.IsVisible = true;
				loader.IsRunning = false;
			}
		}

		public void GoToRegister(object sender, EventArgs e)
		{
			Navigation.PushAsync(new RegisterPage());
		}
	}
}

