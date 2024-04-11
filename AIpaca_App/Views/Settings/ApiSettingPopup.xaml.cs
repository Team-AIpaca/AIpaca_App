using CommunityToolkit.Maui.Views;

namespace AIpaca_App.Views.Settings;

public partial class ApiSettingPopup : Popup
{
	public ApiSettingPopup()
	{
		InitializeComponent();
        // ���� API Ű�� Preferences���� �ҷ��ͼ� ���̺� ǥ���մϴ�.
        CurrentApiKeyLabel.Text = Preferences.Get("GeminiApiKey", "API Ű�� �������� �ʾҽ��ϴ�.");
    }
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // ����ڰ� �Է��� API Ű�� ����
        var apiKey = ApiKeyEntry.Text;
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            Preferences.Set("GeminiApiKey", apiKey);

            // ���� �� �˾��� �ݽ��ϴ�.
            this.Close();

            // ���� Ȯ�� �޽����� ǥ���մϴ�.
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert("���� �Ϸ�", "���ο� API Ű�� ����Ǿ����ϴ�.", "Ȯ��");
            }
        }
        else
        {
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert("���", "��ȿ�� API Ű�� �Է����ּ���.", "Ȯ��");
            }
        }
    }
}