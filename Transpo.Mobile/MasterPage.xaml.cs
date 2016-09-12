using System.Collections.Generic;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;

namespace Transpo.Mobile
{
	public partial class MasterPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		public string UserName
		{
			get
			{
				var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
				return (account != null) ? account.Username : null;
			}
		}

		public void Refresh()
		{
			var masterPageItems = new List<MasterPageItem>();
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Превози",
				//IconSource = "contacts.png",
				TargetType = typeof(RidesPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Пребарај",
				TargetType = typeof(SearchPage)
			});

			if (string.IsNullOrEmpty(UserName))
			{
				masterPageItems.Add(new MasterPageItem
				{
					Title = "Најави се",
					TargetType = typeof(LoginPage)
				});
			}
			else
			{
				masterPageItems.Add(new MasterPageItem
				{
					Title = "Понуди превоз",
					TargetType = typeof(CreateRidePage)
				});

				masterPageItems.Add(new MasterPageItem
				{
					Title = "Одјави се",
					TargetType = typeof(LogoutPage)
				});
			}

			listView.ItemsSource = masterPageItems;
		}

		public MasterPage()
		{
			InitializeComponent();

			MessagingCenter.Subscribe<ContentPage>(this, "Refresh", (s) => { Refresh(); });

			Refresh();
		}
	}
}
