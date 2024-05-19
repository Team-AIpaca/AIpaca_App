using Microsoft.Maui.Controls.Compatibility;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using AIpaca_App.Views.Account;
using AIpaca_App.ViewModels;
using AIpaca_App.Views.Settings;
using AIpaca_App.Data;
using AIpaca_App.Models;
using AIpaca_App.Resources.Localization;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace AIpaca_App.Views;

public partial class SettingsPage : ContentPage
{
    private SettingViewModel _viewModel;

    public SettingsPage()
    {
        InitializeComponent();
        _viewModel = new SettingViewModel();
        this.BindingContext = _viewModel;
    }

    // 팝업을 보여주는 메서드에서 배경색을 복구하도록 수정
    private async Task ShowPopupAndRestoreBackgroundColor(Func<Task> showPopupFunc)
    {
        var originalColor = this.BackgroundColor; // 현재 배경색 저장
        await showPopupFunc(); // 팝업 표시
        this.BackgroundColor = originalColor; // 배경색 복구
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
            view.BackgroundColor = Color.FromArgb("#E0E0E0").MultiplyAlpha((float)0.5); // 임시 리플 색상
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
    private async void OnCreateAPIkeyButtonClicked(object sender, EventArgs e)
    {
        var createAPIkey = new ApiCreatePopup();
        await this.ShowPopupAsync(createAPIkey);
    }

    private async void OnLogButtonClicked(object sender, EventArgs e)
    {
        var errorLogPage = new LogPage();
        await this.Navigation.PushAsync(errorLogPage);
    }

    private void OnDarkModeTouchGestureCompleted(object sender, EventArgs e)
    {
        // Switch의 현재 상태를 반전하여 토글 상태를 변경합니다.
        DarkModeToggle.IsToggled = !DarkModeToggle.IsToggled;
    }
    private void OnLowPowerModeTouchGestureCompleted(object sender, EventArgs e)
    {
        // Switch의 현재 상태를 반전하여 토글 상태를 변경합니다.
        LowPowerModeToggle.IsToggled = !LowPowerModeToggle.IsToggled;
    }
    private async void OnManualupdateClicked(object sender, EventArgs e)
    {
        // 수동 업데이트 로직 수행
        try
        {
            // 사용자가 업데이트를 원할 경우 앱 스토어로 리디렉션
            var success = await Launcher.TryOpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.AIpaca&hl=en-US"));
            if (!success)
            {
                // URL을 열 수 없는 경우, 사용자에게 추가적인 알림 제공
                await Toast.Make(AppResources.splash_error_appstore, ToastDuration.Long).Show();
            }
        }
        catch (Exception)
        {
            await Toast.Make(AppResources.error).Show();
        }
    }
    private async void OnPingButtonClicked(object sender, EventArgs e)
    {
        // Ping 서버로부터 응답을 받아오는 로직 수행
        await _viewModel.CheckServerAsync();
    }
    private void OnBackUpButtonClicked(object sender, EventArgs e)
    {
        // 백업 로직 수행
    }
}