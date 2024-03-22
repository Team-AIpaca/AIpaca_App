using System.Diagnostics;

namespace AIpaca_App.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
        // 앱 버전과 빌드 번호를 가져와서 설정 페이지에 표시
        //var version = VersionTracking.CurrentVersion;
        //var build = VersionTracking.CurrentBuild;
        //AppVersionLabel.Text = $"Version {version} (Build {build})";

        // 앱 설정에서 다크 모드 값을 불러와 스위치에 설정합니다.
        DarkModeToggle.IsToggled = Preferences.Get("IsDarkModeEnabled", false);
    }

    private void OnDarkModeToggled(object sender, ToggledEventArgs e)
    {
        Preferences.Set("IsDarkModeEnabled", e.Value);
        if (App.Current != null)
        {
            App.Current.ApplyTheme(e.Value);
        }
    }

    private void LowPowerModeEnabled(object sender, ToggledEventArgs e)
    {
        // 라이트 모드 상태를 저장하고 적용하는 코드
        bool isLowPowerModeEnabled = e.Value;
        // 라이트 모드 상태를 저장하고 적용하는 코드
    }

    private void OnThemeCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radio && e.Value)
        {
            if (e.Value)
            {
                var selectedTheme = radio.Content.ToString();
                // 선택된 테마를 기반으로 테마를 적용하는 코드
            }
        }
    }

    private void OnLanguageCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radio && e.Value)
        {
            if (e.Value)
            {
                var selectedLanguage = radio.Content.ToString();
                // 선택된 언어를 기반으로 언어를 적용하는 코드
            }
        }
    }

}