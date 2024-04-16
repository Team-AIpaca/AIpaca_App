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
            view.BackgroundColor = Color.FromArgb("#E0E0E0").MultiplyAlpha((float)0.5); // ��8888858�� ���� ����
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
}