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
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;
        var countTxt = $"Clicked {count} time" + (count == 1 ? "" : "s");
        CounterBtn.Text = countTxt;
    }

    private async void OnCheckConnectivity(object sender, EventArgs e)
    {
        NetworkAccess status = Connectivity.Current.NetworkAccess;
        await Shell.Current.DisplayAlert("Connectivity Status", $"Current Status = {status}", "OK");
    }

    private void OnInitLogger(object sender, EventArgs e)
    {
        string startupLoggerUrl = LoggerUrl.Text;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Apple", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("DeviceID", "mauiApp")
            .Enrich.WithClientAgent()
            .Enrich.WithClientIp()
            .Enrich.WithUserName()
            .Enrich.WithAssemblyName()
            .Enrich.WithAssemblyVersion()
            .Enrich.WithThreadId()
            .WriteTo.Seq(startupLoggerUrl)
            .CreateLogger();

        LoggerFactory loggerFactory = new();
        loggerFactory.AddSerilog(Log.Logger);
        logger = loggerFactory.CreateLogger<App>();
    }

    private void OnTestLogger(object sender, EventArgs e)
    {
        logger?.LogInformation("Logger Test Message : count = {count} ",count);
    }

}

