using Microsoft.Extensions.Logging;
using BingeBuddy.Pages;

namespace BingeBuddy
{
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

            // Register pages for navigation
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<AddMoviePage>();
            builder.Services.AddSingleton<NotificationsPage>();
            builder.Services.AddSingleton<ProfilePage>();
            builder.Services.AddSingleton<SettingsPage>();

            return builder.Build();
        }
    }
}
