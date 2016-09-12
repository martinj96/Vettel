using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using YourNamespace.iOS.View.Controls;

[assembly: ExportRenderer(typeof(TimePicker), typeof(TimePicker24HRenderer))]
namespace YourNamespace.iOS.View.Controls
{
	public class TimePicker24HRenderer : TimePickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
		{
			base.OnElementChanged(e);
			var timePicker = (UIDatePicker)Control.InputView;
			timePicker.Locale = new NSLocale("no_nb");
		}
	}
}