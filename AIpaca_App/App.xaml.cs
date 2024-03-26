using AIpaca_App.Data;
using AIpaca_App.Models;
using AIpaca_App.Resources.Splash;
using AIpaca_App.Views;

namespace AIpaca_App
{
    public partial class App : Application
    {

        static DatabaseHelper? database;
        public static DatabaseHelper Database
        {
            get
            {
                if (database == null)
                {
                    database = new DatabaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "achievements.db3"));
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

        protected override async void OnStart()
        {
            await InitializeDatabaseAsync();
            base.OnStart();
        }

        private async Task InitializeDatabaseAsync()
        {
            var achievements = await Database.GetAchievementsAsync();
            if (achievements.Count == 0)
            {
                await InsertInitialAchievementsDataAsync();
            }
        }

        private async Task InsertInitialAchievementsDataAsync()
        {
            var newAchievements = new List<Achievement>
            {
                new Achievement { Key = "acv_0001", Korean = "시작이 반이다!", English = "Half the Battle!", Japanese = "" },
                // 이하 생략, 다른 업적 데이터도 이와 같은 방식으로 리스트에 추가
            };

            foreach (var ach in newAchievements)
            {
                await Database.SaveAchievementAsync(ach);
            }
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