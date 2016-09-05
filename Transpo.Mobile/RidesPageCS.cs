using Xamarin.Forms;

namespace Transpo.Mobile
{
	public class RidesPageCS : ContentPage
	{
		public RidesPageCS()
		{
			Title = "Rides Page";
			Content = new StackLayout
			{
				Children = {
					new Label {
						Text = "Rides list data goes here",
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}
