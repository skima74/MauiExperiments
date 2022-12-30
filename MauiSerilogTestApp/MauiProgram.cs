using Microsoft.Extensions.Logging;
using Serilog;

namespace MauiSerilogTestApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        string startupLoggerUrl = "http://10.0.0.105:4004";

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

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .Logging.AddSerilog(Log.Logger);


        return builder.Build();
    }
}
