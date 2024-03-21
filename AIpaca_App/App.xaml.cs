using AIpaca_App.Views;

namespace AIpaca_App
{
    public partial class App : Application
    {
        public static new App? Current => Application.Current as App;
        public App()
        {
            InitializeComponent();
            ApplyTheme(Preferences.Get("IsDarkModeEnabled", false));
            MainPage = new AIpaca_App.Resources.Splash.SplashPage();
        }
        public void ApplyTheme(bool isDarkModeEnabled)
        {
            UserAppTheme = isDarkModeEnabled ? AppTheme.Dark : AppTheme.Light;
        }
    }
}