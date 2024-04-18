using AIpaca_App.Resources.Localization;
using CommunityToolkit.Maui.Views;

namespace AIpaca_App.Views.Settings;

public partial class ApiSettingPopup : Popup
{
	public ApiSettingPopup()
	{
		InitializeComponent();
        // 현재 API 키를 Preferences에서 불러와서 레이블에 표시합니다.
        CurrentApiKeyLabel.Text = Preferences.Get("GeminiApiKey", "API 키가 설정되지 않았습니다.");
    }
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // 사용자가 입력한 API 키를 저장
        var apiKey = ApiKeyEntry.Text;
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            Preferences.Set("GeminiApiKey", apiKey);

            // 저장 후 팝업을 닫습니다.
            this.Close();
            
            // 저장 확인 메시지를 표시합니다.
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