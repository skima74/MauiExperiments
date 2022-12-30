using System.Net.NetworkInformation;
using System.Text;

namespace MauiTestPingApp;

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

    private async void OnPingClicked(object sender, EventArgs e)
    {
        
        var pingSender = new Ping();
        var options = new PingOptions
        {
            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            DontFragment = true
        };

        // Create a buffer of 32 bytes of data to be transmitted.
        string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        byte[] buffer = Encoding.ASCII.GetBytes(data);
        int timeout = 120;
        var url = PingUrlEntry.Text;
        
        PingReply reply = pingSender.Send(url, timeout, buffer, options);
        string result = "No reply data.";
        string heading = "Ping : " + reply.Status.ToString();
        if (reply.Status == IPStatus.Success)
        {
            result =  "Address: " + reply.Address + Environment.NewLine;
            result += "RoundTrip time: " + reply.RoundtripTime + Environment.NewLine;
            result += "Time to live: "+ reply.Options.Ttl + Environment.NewLine;
            result += "Don't fragment: "+ reply.Options.DontFragment + Environment.NewLine;
            result += "Buffer size: "+ reply.Buffer.Length;
        }
        await Shell.Current.DisplayAlert(heading, result, "OK");
    }
}

