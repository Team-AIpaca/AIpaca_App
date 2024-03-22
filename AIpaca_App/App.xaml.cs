using AIpaca_App.Resources.Splash;
using AIpaca_App.Views;

namespace AIpaca_App
{
    public partial class App : Application
    {
        public static new App? Current => Application.Current as App;
        public App()
        {
            InitializeComponent();
            LoadPreferences();
            MainPage = new SplashPage();
        }

        private void LoadPreferences()
        {
            // 다크 모드 설정을 불러옵니다.
            var isDarkModeEnabled = Preferences.Get("IsDarkModeEnabled", false);
            // 다크 모드 적용 로직 (예시)
            ApplyTheme(isDarkModeEnabled);

            // 여기에 다른 설정 값들을 불러오고 적용하는 코드를 추가할 수 있습니다.
        }

        public void ApplyTheme(bool isDarkMode)
        {
            var currentTheme = isDarkMode ? AppTheme.Dark : AppTheme.Light;
            UserAppTheme = currentTheme;
            SetPagesBackgroundColor(currentTheme);
        }

        private void SetPagesBackgroundColor(AppTheme theme)
        {
            // 여기에서 모든 페이지에 대한 배경색을 설정하는 코드를 작성할 수 있습니다.
            // 예제로, MainPage의 배경색을 변경합니다.
            if (MainPage is SplashPage splashPage)
            {
                splashPage.BackgroundColor = theme == AppTheme.Dark ? Color.FromArgb("#212121") : Colors.White;
            }
            // 다른 페이지들에 대해서도 비슷하게 적용할 수 있습니다.
        }
    }
}