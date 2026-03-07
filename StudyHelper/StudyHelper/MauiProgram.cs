using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StudyHelper.Chat;
using StudyHelper.Vision;

namespace StudyHelper;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        // Load configuration using Shiny's platform bundle support
        // This loads appsettings.json from platform-specific locations:
        // - Android: Assets folder
        // - iOS/Mac: Bundle Resources
        // - Windows: Embedded resources
        // In DEBUG mode, also loads appsettings.Development.json for local API keys
#if DEBUG
        builder.Configuration.AddJsonPlatformBundle("Development");
#else
		builder.Configuration.AddJsonPlatformBundle();
#endif

        builder.Services.AddSingleton<ChatService>();
        builder.Services.AddSingleton<VisionService>();

        return builder.Build();
    }
}