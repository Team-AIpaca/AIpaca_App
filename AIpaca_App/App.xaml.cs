using AIpaca_App.Data;
using AIpaca_App.Models;
using AIpaca_App.Resources.Splash;
using AIpaca_App.Views;
using System.ComponentModel;

namespace AIpaca_App
{
    public partial class App : Application, INotifyPropertyChanged
    {
        static DatabaseHelper? database;
        public static DatabaseHelper Database
        {
            get
            {
                if (database == null)
                {
                    database = new DatabaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AchieveList.db3"));
                }
                return database;
            }
        }

        public static new App? Current => Application.Current as App;
        public App()
        {
            InitializeComponent();
            LoadPreferences();
            MainPage = new SplashPage();
            VersionTracking.Track();
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

            // 페이지 배경색을 다크 모드에 맞게 변경합니다.
            var backgroundColor = isDarkMode ? Color.FromArgb("#121212") : Color.FromArgb("#fafafa");
            if (Resources.TryGetValue("DefaultPageBackgroundColor", out var resource))
            {
                Resources["DefaultPageBackgroundColor"] = backgroundColor;
            }
        }
    }
}