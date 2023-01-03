using Emgu.CV;

namespace MauiEmguCvInit;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

    private void OnInitClicked(object sender, EventArgs e)
    {
#if __IOS__
        CvInvokeIOS.Init();
#elif __ANDROID__
        CvInvokeAndroid.Init();
#endif
    }
}

