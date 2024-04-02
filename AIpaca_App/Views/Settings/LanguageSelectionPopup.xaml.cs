using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Globalization;

namespace AIpaca_App.Views.Settings;

public partial class LanguageSelectionPopup : Popup
{
    public LanguageSelectionPopup()
    {
        InitializeComponent();
    }
    private void OnKoreanSelected(object sender, EventArgs e)
    {
        // 언어 설정을 'LanguageCode' 키로 저장합니다.
        Preferences.Set("LanguageCode", "ko");
        ChangeAppLanguage("ko");
    }

    private void OnEnglishSelected(object sender, EventArgs e)
    {
        // 언어 설정을 'LanguageCode' 키로 저장합니다.
        Preferences.Set("LanguageCode", "en");
        ChangeAppLanguage("en");
    }

    private void ChangeAppLanguage(string languageCode)
    {
        CultureInfo.CurrentCulture = new CultureInfo(languageCode);
        CultureInfo.CurrentUICulture = new CultureInfo(languageCode);

        // 팝업을 닫습니다.
        Close();

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
                    await Shell.Current.GoToAsync("//settingsPage");
                });
            });
        }
    }
}
