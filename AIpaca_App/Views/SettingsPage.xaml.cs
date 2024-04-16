using Microsoft.Maui.Controls.Compatibility;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using AIpaca_App.Views.Account;
using AIpaca_App.ViewModels;
using AIpaca_App.Views.Settings;
namespace AIpaca_App.Views;

public partial class SettingsPage : ContentPage
{
    private MainViewModel _viewModel;
    public SettingsPage()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
        this.BindingContext = _viewModel;
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

    #region 로그인 관련 버튼
    //로그인 버튼 클릭
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        var loginPopup = new LoginPagePopup(_viewModel); // LoginPage는 로그인 폼을 구현한 별도의 ContentPage입니다.
        await this.ShowPopupAsync(loginPopup);
    }

    //회원가입 버튼 클릭
    private async void OnSignupButtonClicked(object sender, EventArgs e)
    {
        var signupPopup = new SignupPagePopup(_viewModel); // SignupPage는 회원가입 폼을 구현한 별도의 ContentPage입니다.
        await this.ShowPopupAsync(signupPopup);
    }

    //로그아웃 버튼 클릭
    private void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        // 로그아웃 로직 수행
        _viewModel.Logout();
    }
    #endregion

    //언어설정 버튼 클릭
    private async void OnLanguageSettingsClicked(object sender, EventArgs e)
    {
        var languagePopup = new LanguageSelectionPopup();
        await this.ShowPopupAsync(languagePopup);
    }

    private async void OnAPISettingButtonClicked(object sender, EventArgs e)
    {
        var apisettingpopup = new ApiSettingPopup();
        await this.ShowPopupAsync(apisettingpopup);
    }
}