using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Xamarin.Auth;
using Xamarin.Forms;
using Newtonsoft.Json;
using Transpo.Models;
using System.Text;

namespace Transpo.Mobile
{
	public partial class RegisterPage : ContentPage
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

		public string ConfirmPassword
		{
			get
			{
				return confirmPassword.Text;
			}
		}


		public string Phone
		{
			get
			{
				return phone.Text;
			}
		}

		public string Name
		{
			get
			{
				return name.Text;
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


		public RegisterPage()
		{
			InitializeComponent();
		}

		public void OnRegisterSubmit(object sender, EventArgs e)
		{
			loader.IsRunning = true;

			var emailRegex = new Regex("^\\S+@\\S+$");
			if (string.IsNullOrEmpty(Email) || emailRegex.Match(Email).Success == false)
			{
				lblError.Text = "Внесовте погрешна email адреса";
				lblError.IsVisible = true;
				loader.IsRunning = false;
				return;
			}

			if (String.IsNullOrEmpty(Password) || !String.Equals(Password, ConfirmPassword) || Password.Length < 6)
			{
				lblError.Text = "Внесовте невалидна лозинка";
				lblError.IsVisible = true;
				loader.IsRunning = false;
				return;
			}

			if (String.IsNullOrEmpty(Name))
			{
				lblError.Text = "Внесете име";
				lblError.IsVisible = true;
				loader.IsRunning = false;
				return;
			}

			if (String.IsNullOrEmpty(Phone) || Phone.Length != 9 || !Phone.StartsWith("07")) 
			{
				lblError.Text = "Внесовте невалиден телефонски број";
				lblError.IsVisible = true;
				loader.IsRunning = false;
				return;
			}

			var client = new HttpClient();
			client.BaseAddress = new Uri("http://api.kinisaj.mk/");
			var model = new RegisterViewModel
			{
				Email = Email,
				Password = Password,
				ConfirmPassword = ConfirmPassword,
				Name = Name,
				Phone = Phone
			};
			var json = JsonConvert.SerializeObject(model);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = client.PostAsync("account/register", content).Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var jsonResult = response.Content.ReadAsStringAsync().Result;
				if (string.IsNullOrEmpty(UserName))
				{
					Account account = new Account
					{
						Username = Email
					};
					account.Properties.Add("Password", Password);
					account.Properties.Add("Model", jsonResult);
					AccountStore.Create().Save(account, App.AppName);

					MessagingCenter.Send<ContentPage>(this, "Refresh");
					MessagingCenter.Send<ContentPage>(this, "NavigateToRides");
				}
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

