using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;

namespace ValiantTemplate.Photino;

public static class Program
{
    public static bool IsDevelopment { get; }
#if DEBUG
    = true;
#endif

    [STAThread]
    static void Main(string[] args)
    {
        var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);
        appBuilder.RootComponents.Add<Routes>("app");

        // Override the builder when we're in development mode.
        if (IsDevelopment)
        {
            appBuilder.Services.Configure<PhotinoBlazorAppConfiguration>(opts =>
            {
                opts.AppBaseUri = new Uri(PhotinoWebViewManager.AppBaseUri);
                opts.HostPage = "index.dev.html";
            });
        }

        var app = appBuilder.Build();

        app.MainWindow
            .SetTitle("ValiantTemplate")
            .SetUseOsDefaultSize(false)
            .SetSize(1024, 768)
            .SetMinSize(640, 480)
            .Center();

        if (IsDevelopment)
        {
            app.MainWindow
                .SetDevToolsEnabled(true)
                .SetContextMenuEnabled(true);
        }

        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
        {
            app.MainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
        };

        app.Run();
    }
}
