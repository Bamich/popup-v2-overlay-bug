using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;

namespace ShellTest;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnClicked(object? sender, EventArgs e)
	{
		Dispatcher.DispatchAsync(async () =>
		{
			// Be sure to specify the background color to see the overlay.
			var options = new PopupOptions()
			{
				PageOverlayColor = Colors.Green
			};

			// It doesn't matter what to show.
			var view = new Label
			{
				Text = "Tap to dismiss this popup",
				BackgroundColor = Colors.Red
			};

			// 1. Show the popup without waiting for it to close
			_ = Shell.Current.ShowPopupAsync(view, options);

			// 2. Wait for 1 frame.
			await Task.Yield();

			// 3. Finally, pop to root. The animated flag does not matter.
			await Shell.Current.Navigation.PopToRootAsync(false);

			/* 
			 * Now the popup itself is gone, but we see it's green overlay.
			 * The model stack is empty. Optionaly we can navigate to another page 
			 * in the regular navigation stack and see that the page has changed 
			 * under overlay. Essentially, the app is frozen and won't respond to touch.
			*/
			await Shell.Current.Navigation.PushAsync(new Page1());
		});
	}
}
