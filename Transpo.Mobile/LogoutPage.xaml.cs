using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;

namespace Transpo.Mobile
{
	public partial class LogoutPage : ContentPage
	{
		public void Logout()
		{
			var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();

			if (account != null)
			{
				AccountStore.Create().Delete(account, App.AppName);
			}
		}

		public LogoutPage()
		{
			Logout();
			MessagingCenter.Send<ContentPage>(this, "Refresh");
			MessagingCenter.Send<ContentPage>(this, "NavigateToRides");
			InitializeComponent();
		}
	}
}

