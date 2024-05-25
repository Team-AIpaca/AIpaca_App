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

    #region api �����ư Ŭ�� �̺�Ʈ
    private async void OnGPTAPISaveClicked(object sender, EventArgs e)
    {
        try
        {
            string GPTapiKey = GPTApiKeyEntry.Text;
            
            // null üũ
            if (!string.IsNullOrWhiteSpace(GPTapiKey))
            {
                // ������Ʈ �˾� ����
                var updatePopup = new AlertPopup
                {
                    MainText = AppResources.check_api_save,
                    btn1Text = AppResources.ok
                };
                // Ȯ�� ��ư Ŭ���� ��� api ���� �޼��� ����
                updatePopup.btn1Clicked += async (sender, e) => await _viewModel.SaveGPTApiKey(GPTapiKey);

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

    private async void OnGeminiAPISaveClicked(object sender, EventArgs e)
    {
        try
        {
            var GeminiapiKey = GeminiApiKeyEntry.Text;

            // null üũ
            if (!string.IsNullOrWhiteSpace(GeminiapiKey))
            {
                // ������Ʈ �˾� ����
                var updatePopup = new AlertPopup
                {
                    MainText = AppResources.check_api_save,
                    btn1Text = AppResources.ok
                };
                // Ȯ�� ��ư Ŭ���� ��� api ���� �޼��� ����
                updatePopup.btn1Clicked += async (sender, e) => await _viewModel.SaveGeminiApiKey(GeminiapiKey);

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

    #endregion

    #region api ������ư Ŭ�� �̺�Ʈ
    private async void OnGPTAPIDeleteClicked(object sender, EventArgs e)
    {
        try
        {
            // ������Ʈ �˾� ����
            var updatePopup = new AlertPopup
            {
                
                MainText = AppResources.check_api_del,
                btn1Text = AppResources.ok
            };
            // Ȯ�� ��ư Ŭ���� ��� api ���� �޼��� ����
            updatePopup.btn1Clicked += async (sender, e) => await _viewModel.DeleteGPTApiKey();

            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.ShowPopupAsync(updatePopup);
            }
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message).Show();
        }
    }

    private async void OnGeminiAPIDeleteClicked(object sender, EventArgs e)
    {
        try
        {
            // ������Ʈ �˾� ����
            var updatePopup = new AlertPopup
            {
                MainText = AppResources.check_api_del,
                btn1Text = AppResources.ok
            };
            // Ȯ�� ��ư Ŭ���� ��� api ���� �޼��� ����
            updatePopup.btn1Clicked += async (sender, e) => await _viewModel.DeleteGeminiApiKey();

            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.ShowPopupAsync(updatePopup);
            }
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message).Show();
        }
    }
    #endregion

    private void OnCloseClicked(object sender, EventArgs e)
    {
        this.Close();
    }
}
