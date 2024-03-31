using Microsoft.Maui.Controls.Compatibility;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using AIpaca_App.Views.Account;
namespace AIpaca_App.Views;

public partial class SettingsPage : ContentPage
{
    //�� ������ ������
    public string AppVersion => VersionTracking.CurrentVersion;
    //�� ���� ��ȣ�� ������
    public string AppBuild => VersionTracking.CurrentBuild;

    public SettingsPage()
    {
        InitializeComponent();

        // �� ������ ���� ��ȣ�� �����ͼ� ���� �������� ǥ��
        BindingContext = this; // SettingsPage�� BindingContext�� �����մϴ�.

        // �� �������� ��ũ ��� ���� �ҷ��� ����ġ�� �����մϴ�.
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
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        var loginPopup = new LoginPagePopup(); // LoginPage�� �α��� ���� ������ ������ ContentPage�Դϴ�.
        await this.ShowPopupAsync(loginPopup);
    }
    private async void OnSignupButtonClicked(object sender, EventArgs e)
    {
        var signupPopup = new SignupPagePopup(); // SignupPage�� ȸ������ ���� ������ ������ ContentPage�Դϴ�.
        await this.ShowPopupAsync(signupPopup);
    }
}