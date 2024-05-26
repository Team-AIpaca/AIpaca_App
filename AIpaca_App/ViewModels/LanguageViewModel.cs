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
        public async Task LanguageSet(string LanguageCode)
        {
            // 언어 설정을 'LanguageCode' 키로 저장합니다.
            Preferences.Set("LanguageCode", LanguageCode);
            ChangeAppLanguage(LanguageCode);
            await Toast.Make(AppResources.success).Show();
        }

        private void ChangeAppLanguage(string languageCode)
        {
            CultureInfo.CurrentCulture = new CultureInfo(languageCode);
            CultureInfo.CurrentUICulture = new CultureInfo(languageCode);

            // 앱의 메인 페이지 또는 다른 UI 요소를 다시 로드하여 변경 사항을 적용합니다.
            // 예: Application.Current.MainPage = new MainPage();
            // 이 방법은 앱의 구조와 필요에 따라 조정해야 할 수 있습니다.
            if (Application.Current != null)
            {
                Application.Current.MainPage = new AppShell();
                Task.Run(async () =>
                {
                    await Application.Current.MainPage.Dispatcher.DispatchAsync(async () =>
                    {
                        await Shell.Current.GoToAsync("//SettingsPage");
                    });
                });
            }
        }
    }
}
