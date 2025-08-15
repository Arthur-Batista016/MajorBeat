using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

#if ANDROID
using AndroidX.AppCompat.Widget; // SearchView do AndroidX
using Android.Widget;            // ImageView
using Microsoft.Maui.Platform;   // handler.PlatformView
#endif

namespace MajorBeat
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Remove o ícone de lupa no SearchBar
            Microsoft.Maui.Handlers.SearchBarHandler.Mapper.AppendToMapping("NoIcon", (handler, view) =>
            {
#if ANDROID
                // Pegando a SearchView nativa do AndroidX
                var searchView = handler.PlatformView as AndroidX.AppCompat.Widget.SearchView;
                if (searchView != null)
                {
                    int searchIconId = searchView.Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                    var searchIcon = searchView.FindViewById<ImageView>(searchIconId);
                    searchIcon?.SetImageDrawable(null); // Remove a lupa
                }
#elif IOS
                // Remove a lupa no iOS
                handler.PlatformView.SearchTextField.LeftView = null;
#endif
            });

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

            return builder.Build();
        }
    }
}
