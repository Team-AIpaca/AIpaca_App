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

    // �˾��� �����ִ� �޼��忡�� ������ �����ϵ��� ����
    private async Task ShowPopupAndRestoreBackgroundColor(Func<Task> showPopupFunc)
    {
        var originalColor = this.BackgroundColor; // ���� ���� ����
        await showPopupFunc(); // �˾� ǥ��
        this.BackgroundColor = originalColor; // ���� ����
    }

    private void LowPowerModeEnabled(object sender, ToggledEventArgs e)
    {
        // ����Ʈ ��� ���¸� �����ϰ� �����ϴ� �ڵ�
        bool isLowPowerModeEnabled = e.Value;
        // ����Ʈ ��� ���¸� �����ϰ� �����ϴ� �ڵ�
    }

    private void OnThemeCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radio && e.Value)
        {
            if (e.Value)
            {
                var selectedTheme = radio.Content.ToString();
                // ���õ� �׸��� ������� �׸��� �����ϴ� �ڵ�
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
                // ���õ� �� ������� �� �����ϴ� �ڵ�
            }
        }
    }

    private async void OnRippleEffectRequested(object sender, EventArgs e)
    {
        if (sender is View view)
        {
            // �ʱ� ���� ����
            var originalColor = view.BackgroundColor;

            // �ִϸ��̼� ����
            await view.ScaleTo(0.95, 50, Easing.SinInOut);
            view.BackgroundColor = Color.FromArgb("#E0E0E0").MultiplyAlpha((float)0.5); // �ӽ� ���� ����
            await view.ScaleTo(1, 50, Easing.SinInOut);

            // ���� �������� ����
            view.BackgroundColor = originalColor;
        }
    }

    #region �α��� ���� ��ư
    //�α��� ��ư Ŭ��
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        var loginPopup = new LoginPagePopup(_viewModel); // LoginPage�� �α��� ���� ������ ������ ContentPage�Դϴ�.
        await this.ShowPopupAsync(loginPopup);
    }

    //ȸ������ ��ư Ŭ��
    private async void OnSignupButtonClicked(object sender, EventArgs e)
    {
        var signupPopup = new SignupPagePopup(_viewModel); // SignupPage�� ȸ������ ���� ������ ������ ContentPage�Դϴ�.
        await this.ShowPopupAsync(signupPopup);
    }

    //�α׾ƿ� ��ư Ŭ��
    private void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        // �α׾ƿ� ���� ����
        _viewModel.Logout();
    }
    #endregion


    //���� ��ư Ŭ��
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
        // Switch�� ���� ���¸� �����Ͽ� ��� ���¸� �����մϴ�.
        DarkModeToggle.IsToggled = !DarkModeToggle.IsToggled;
    }
    private void OnLowPowerModeTouchGestureCompleted(object sender, EventArgs e)
    {
        // Switch�� ���� ���¸� �����Ͽ� ��� ���¸� �����մϴ�.
        LowPowerModeToggle.IsToggled = !LowPowerModeToggle.IsToggled;
    }
    private async void OnManualupdateClicked(object sender, EventArgs e)
    {
        // ���� ������Ʈ ���� ����
        try
        {
            // ����ڰ� ������Ʈ�� ���� ��� �� ������ ���𷺼�
            var success = await Launcher.TryOpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.AIpaca&hl=en-US"));
            if (!success)
            {
                // URL�� �� �� ���� ���, ����ڿ��� �߰����� �˸� ����
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
        // Ping �����κ��� ������ �޾ƿ��� ���� ����
        await _viewModel.CheckServerAsync();
    }
    private void OnBackUpButtonClicked(object sender, EventArgs e)
    {
        // ��� ���� ����
    }
}