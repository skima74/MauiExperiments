using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MauiSerilogTestApp;

public partial class MainPage : ContentPage
{
    int count = 0;
    private ILogger logger;
    public MainPage()
    {
        InitializeComponent();
        LoggerFactory loggerFactory = new();
        loggerFactory.AddSerilog(Log.Logger);
        logger = loggerFactory.CreateLogger<App>();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;
        var countTxt = $"Clicked {count} time" + (count == 1 ? "" : "s");
        CounterBtn.Text = countTxt;
        logger?.LogInformation(countTxt);
    }

    private async void OnCheckConnectivity(object sender, EventArgs e)
    {
        NetworkAccess status = Connectivity.Current.NetworkAccess;
        //PermissionStatus status = await Permissions.CheckStatusAsync<Permissions>();
        
        await Shell.Current.DisplayAlert("Connectivity Status", $"Current Status = {status}", "OK");
    }
}

