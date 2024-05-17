using AIpaca_App.Resources.Localization;
using AIpaca_App.ViewModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace AIpaca_App.Views.Settings;

public partial class ApiSettingPopup : Popup
{
    private ApiSettingViewModel _viewModel;

    public ApiSettingPopup()
    {
        InitializeComponent();
        _viewModel = new ApiSettingViewModel();
        BindingContext = _viewModel;
    }

    private async void OnGPTAPISaveClicked(object sender, EventArgs e)
    {
        var GPTapiKey = GPTApiKeyEntry.Text;
        if (!string.IsNullOrWhiteSpace(GPTapiKey))
        {
            _viewModel.SaveGPTApiKey(GPTapiKey);
            // Close the popup
            this.Close();

            // Show a confirmation message
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
    
    private async void OnGeminiAPISaveClicked(object sender, EventArgs e)
    {
        var GeminiapiKey = GeminiApiKeyEntry.Text;
        if (!string.IsNullOrWhiteSpace(GeminiapiKey))
        {
            _viewModel.SaveGeminiApiKey(GeminiapiKey);
            // Close the popup
            this.Close();

            // Show a confirmation message
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
