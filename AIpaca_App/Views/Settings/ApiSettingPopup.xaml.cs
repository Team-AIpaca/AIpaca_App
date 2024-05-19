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
        try
        {
            string GPTapiKey = GPTApiKeyEntry.Text;
            if (!string.IsNullOrWhiteSpace(GPTapiKey))
            {
                var updatePopup = new AlertPopup
                {
                    MainText = "Change API Key?",
                    btn1Text = AppResources.ok
                };

                EventHandler? handler = null;
                handler = async (s, args) =>
                {
                    await OnGPTAPISave(GPTapiKey);
                    updatePopup.btn1Clicked -= handler;  // Remove handler after execution
                };
                updatePopup.btn1Clicked += handler;

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
        catch (Exception ex)
        {
            await Toast.Make(ex.Message).Show();
        }
    }

    private async Task OnGPTAPISave(string GPTapiKey)
    {
        _viewModel.SaveGPTApiKey(GPTapiKey);
        

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

    private async void OnGeminiAPISaveClicked(object sender, EventArgs e)
    {
        var GeminiapiKey = GeminiApiKeyEntry.Text;
        if (!string.IsNullOrWhiteSpace(GeminiapiKey))
        {
            _viewModel.SaveGeminiApiKey(GeminiapiKey);

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

    private void OnGPTAPIDeleteClicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var updatePopup = new AlertPopup
            {
                MainText = "Delete API Key?",
                btn1Text = AppResources.ok
            };

            EventHandler? handler = null;
            handler = (s, args) =>
            {
                _viewModel.DeleteGPTApiKey();
                updatePopup.btn1Clicked -= handler;  // Remove handler after execution
            };
            updatePopup.btn1Clicked += handler;

            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.ShowPopupAsync(updatePopup);
            }
        });
    }

    private void OnGeminiAPIDeleteClicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var updatePopup = new AlertPopup
            {
                MainText = "Delete API Key?",
                btn1Text = AppResources.ok
            };

            EventHandler? handler = null;
            handler = (s, args) =>
            {
                _viewModel.DeleteGeminiApiKey();
                updatePopup.btn1Clicked -= handler;  // Remove handler after execution
            };
            updatePopup.btn1Clicked += handler;

            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.ShowPopupAsync(updatePopup);
            }
        });
    }

    private void OnCloseClicked(object sender, EventArgs e)
    {
        this.Close();
    }
}
