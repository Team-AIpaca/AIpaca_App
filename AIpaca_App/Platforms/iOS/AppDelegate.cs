using Foundation;
using UIKit;

namespace AIpaca_App
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        //색상 변경 코드 추가
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // 상단 상태 표시줄 색상 변경
            UINavigationBar.Appearance.BarTintColor = UIColor.Red; // 예: 빨간색
            return base.FinishedLaunching(application, launchOptions);
            // 하단바 색상은 AppShell.xaml에서 수정해야함
        }
    }
}
