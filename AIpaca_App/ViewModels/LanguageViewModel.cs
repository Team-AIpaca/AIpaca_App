using AIpaca_App.Resources.Localization;
using CommunityToolkit.Maui.Alerts;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIpaca_App.ViewModels
{
    public class LanguageViewModel : BaseViewModel
    {
        public void change(string code)
        {
            Preferences.Set("LanguageCode", code);
            ChangeAppLanguage(code);
        }

        private void ChangeAppLanguage(string languageCode)
        {
            CultureInfo.CurrentCulture = new CultureInfo(languageCode);
            CultureInfo.CurrentUICulture = new CultureInfo(languageCode);

            if (Application.Current != null)
            {
                Application.Current.MainPage = new AppShell();
                Task.Run(async () =>
                {
                    await Application.Current.MainPage.Dispatcher.DispatchAsync(async () =>
                    {
                        //SettingsPage로 이동
                        await Shell.Current.GoToAsync("//SettingsPage");
                    });
                });
            }
        }
    }
}