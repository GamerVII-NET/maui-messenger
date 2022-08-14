using Messenger.Client.ViewModels;
using Messenger.Client.Views.Pages;

namespace Messenger.Client
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
                    fonts.AddFont("Inter-Black.ttf", "InterBlack");
                    fonts.AddFont("Inter-Bold.ttf", "InterBold");
                    fonts.AddFont("Inter-ExtraBold.ttf", "InterExtraBold");
                    fonts.AddFont("Inter-ExtraLight.ttf", "InterExtraLight");
                    fonts.AddFont("Inter-Light.ttf", "InterLight");
                    fonts.AddFont("Inter-Medium.ttf", "InterMedium");
                    fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                    fonts.AddFont("Inter-SemiBold.ttf", "InterSemiBold");
                    fonts.AddFont("Inter-Thin.ttf", "InterThin");
                });

            builder.Services.AddSingleton<WelcomePageViewModel>();
            builder.Services.AddSingleton<WelcomePage>();

            return builder.Build();
        }
    }
}