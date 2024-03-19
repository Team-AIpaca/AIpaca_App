namespace AIpaca_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AIpaca_App.Resources.Splash.SplashPage());
        }
    }
}
