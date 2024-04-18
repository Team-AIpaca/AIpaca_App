using AIpaca_App.Resources.Localization;
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
            var updatePopup = new AlertPopup
            {
                MainText = AppResources.api_save_complete,
                btn1Text = AppResources.ok
            };
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.ShowPopupAsync(updatePopup);
            }
        }
        else
        {
            var updatePopup = new AlertPopup
            {
                MainText = AppResources.error_no_api,
                btn1Text = AppResources.ok
            };
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.ShowPopupAsync(updatePopup);
            }
        }
    }
}