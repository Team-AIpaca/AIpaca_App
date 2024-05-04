using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace AIpaca_App
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Window != null)
            {
                // 상태 표시줄 색상 변경
                // Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#FF0000")); // 예: 빨간색
                // 탐색 바 색상 변경
                // Window.SetNavigationBarColor(Android.Graphics.Color.ParseColor("#FF0000")); // 예: 빨간색

                // 상태 표시줄을 투명하게 설정
                //Window.AddFlags(WindowManagerFlags.TranslucentStatus); // 전체 레이아웃 적용
                //Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                Window.SetStatusBarColor(Android.Graphics.Color.Gray);

                // 탐색 바를 투명하게 설정
                Window.SetNavigationBarColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}
