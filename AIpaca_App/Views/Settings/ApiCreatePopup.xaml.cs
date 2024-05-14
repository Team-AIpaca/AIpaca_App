using AIpaca_App.Resources.Localization;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace AIpaca_App.Views.Settings;

public partial class ApiCreatePopup : Popup
{
    public ApiCreatePopup()
    {
        InitializeComponent();
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {

        this.Close();
    }


    #region API 키 생성 링크
    private async void OnCreateAPIKeyClicked(object sender, EventArgs e)
    {
        await Toast.Make("미구현 기능!", ToastDuration.Long).Show();
    }

    private void OnCreateGPTAPIKeyClicked(object sender, EventArgs e)
    {
        OpenUrl("https://platform.openai.com/api-keys");
    }

    private void OnCreateGeminiAPIKeyClicked(object sender, EventArgs e)
    {
        OpenUrl("https://aistudio.google.com/app/apikey");

    }

    private async void OpenUrl(string url)
    {
        try
        {
            var success = await Launcher.OpenAsync(new Uri(url));
            if (!success)
            {
                await Toast.Make(AppResources.splash_error_appstore, ToastDuration.Long).Show();
            }
        }
        catch (Exception)
        {
            await Toast.Make(AppResources.error).Show();
        }
    }
    #endregion
}