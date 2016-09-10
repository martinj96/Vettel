using System;
using Xamarin.Forms;

namespace Transpo.Mobile
{
	public partial class MainPage : MasterDetailPage
	{
		public void NavigateToRides()
		{
			masterPage.ListView.SelectedItem = null;
			Detail = new NavigationPage(new RidesPage());
			IsPresented = false;
		}

		public MainPage()
		{
			InitializeComponent();

			MessagingCenter.Subscribe<ContentPage>(this, "NavigateToRides", (s) =>
			{
				NavigateToRides();
			});

			masterPage.ListView.ItemSelected += OnItemSelected;

			if (Device.OS == TargetPlatform.Windows)
			{
				Master.Icon = "swap.png";
			}
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MasterPageItem;
			if (item != null)
			{
				Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
				masterPage.ListView.SelectedItem = null;
				IsPresented = false;
			}
		}
	}
}