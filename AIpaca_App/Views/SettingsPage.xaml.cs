using Microsoft.Maui.Controls.Compatibility;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using AIpaca_App.Views.Account;
namespace AIpaca_App.Views;

public partial class SettingsPage : ContentPage
{
    //앱 버전을 가져옴
    public string AppVersion => VersionTracking.CurrentVersion;
    //앱 빌드 번호를 가져옴
    public string AppBuild => VersionTracking.CurrentBuild;

    public SettingsPage()
    {
        InitializeComponent();

        // 앱 버전과 빌드 번호를 가져와서 설정 페이지에 표시
        BindingContext = this; // SettingsPage의 BindingContext를 설정합니다.

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

    private async void OnRippleEffectRequested(object sender, EventArgs e)
    {
        if (sender is View view)
        {
            // 초기 색상 저장
            var originalColor = view.BackgroundColor;

            // 애니메이션 실행
            await view.ScaleTo(0.95, 50, Easing.SinInOut);
            view.BackgroundColor = Color.FromArgb("#E0E0E0").MultiplyAlpha((float)0.5); // 임8888858시 리플 색상
            await view.ScaleTo(1, 50, Easing.SinInOut);

            // 원래 색상으로 복원
            view.BackgroundColor = originalColor;
        }
    }
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        var loginPopup = new LoginPagePopup(); // LoginPage는 로그인 폼을 구현한 별도의 ContentPage입니다.
        await this.ShowPopupAsync(loginPopup);
    }
    private async void OnSignupButtonClicked(object sender, EventArgs e)
    {
        var signupPopup = new SignupPagePopup(); // SignupPage는 회원가입 폼을 구현한 별도의 ContentPage입니다.
        await this.ShowPopupAsync(signupPopup);
    }
}