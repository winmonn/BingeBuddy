using Microsoft.Extensions.Logging;
using BingeBuddy.Pages;
using BingeBuddy.ViewModels;

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
            builder.Services.AddSingleton<MovieViewModel>();
            return builder.Build();
        }
    }
}
