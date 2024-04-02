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
        // ��� ������ 'LanguageCode' Ű�� �����մϴ�.
        Preferences.Set("LanguageCode", "ko");
        ChangeAppLanguage("ko");
    }

    private void OnEnglishSelected(object sender, EventArgs e)
    {
        // ��� ������ 'LanguageCode' Ű�� �����մϴ�.
        Preferences.Set("LanguageCode", "en");
        ChangeAppLanguage("en");
    }

    private void ChangeAppLanguage(string languageCode)
    {
        CultureInfo.CurrentCulture = new CultureInfo(languageCode);
        CultureInfo.CurrentUICulture = new CultureInfo(languageCode);

        // �˾��� �ݽ��ϴ�.
        Close();

        // ���� ���� ������ �Ǵ� �ٸ� UI ��Ҹ� �ٽ� �ε��Ͽ� ���� ������ �����մϴ�.
        // ��: Application.Current.MainPage = new MainPage();
        // �� ����� ���� ������ �ʿ信 ���� �����ؾ� �� �� �ֽ��ϴ�.
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
