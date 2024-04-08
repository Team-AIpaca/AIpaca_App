using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace AIpaca_App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();

#if ANDROID
            // Android 플랫폼 코드
            //entry 밑줄 제거
            EntryHandler.Mapper.ModifyMapping("NoUnderline", (handler, entry, action) =>
            {
                var context = handler.PlatformView.Context;
                if (context != null)
                {
                    var colorStateList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
                    handler.PlatformView.BackgroundTintList = colorStateList;
                }
            });
            //picker 밑줄 제거
            PickerHandler.Mapper.ModifyMapping("NoUnderline", (handler, picker, action) =>
            {
                var context = handler.PlatformView.Context;
                if (context != null)
                {
                    var colorStateList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
                    handler.PlatformView.BackgroundTintList = colorStateList;
                }
            });
            //editor 밑줄 제거
            EditorHandler.Mapper.ModifyMapping("NoUnderline", (handler, editor, action) =>
            {
                var context = handler.PlatformView.Context;
                if (context != null)
                {
                    handler.PlatformView.Background = null; // Editor 밑줄 제거
                }
            });
#endif

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}