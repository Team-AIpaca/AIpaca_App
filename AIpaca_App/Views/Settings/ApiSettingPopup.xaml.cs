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

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var apiKey = ApiKeyEntry.Text;
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            _viewModel.SaveApiKey(apiKey);

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

    private async void OnCreateAPIKeyClicked(object sender, EventArgs e)
    {
        await Toast.Make("미구현 기능!", ToastDuration.Long).Show();
        //try
        //{
        //    var success = await Launcher.TryOpenAsync(new Uri("https://aistudio.google.com/app/apikey"));
        //    if (!success)
        //    {
        //        await Toast.Make(AppResources.splash_error_appstore, ToastDuration.Long).Show();
        //    }
        //}
        //catch (Exception)
        //{
        //    await Toast.Make(AppResources.error).Show();
        //}
    }
    private async void OnCreateGeminiAPIKeyClicked(object sender, EventArgs e)
    {
        try
        {
            var success = await Launcher.TryOpenAsync(new Uri("https://aistudio.google.com/app/apikey"));
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
}
