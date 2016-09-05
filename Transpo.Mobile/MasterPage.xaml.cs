using System.Collections.Generic;
using Xamarin.Forms;

namespace Transpo.Mobile
{
	public partial class MasterPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		public MasterPage()
		{
			InitializeComponent();

			var masterPageItems = new List<MasterPageItem>();
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Rides",
				//IconSource = "contacts.png",
				TargetType = typeof(RidesPage)
			});


			listView.ItemsSource = masterPageItems;
		}
	}
}
