using System.Diagnostics;

namespace AIpaca_App.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
        // �� ������ ���� ��ȣ�� �����ͼ� ���� �������� ǥ��
        //var version = VersionTracking.CurrentVersion;
        //var build = VersionTracking.CurrentBuild;
        //AppVersionLabel.Text = $"Version {version} (Build {build})";

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

}