using System.ComponentModel;
using System.Runtime.CompilerServices;
using AIpaca_App.Resources.Localization;
using AIpaca_App.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Storage;
using MvvmHelpers;

namespace AIpaca_App.ViewModels
{
    public class ApiSettingViewModel : BaseViewModel
    {
        private string? _currentApiKey_GPT;
        private string? _currentApiKey_Gemini;

        public ApiSettingViewModel()
        {
            LoadGPTApiKey();
            LoadGeminiApiKey();
        }

        #region 값을 받아오는 메서드
        public string? CurrentApiKey_GPT
        {
            get => _currentApiKey_GPT;
            set => SetProperty(ref _currentApiKey_GPT, value, onChanged: () => OnPropertyChanged(nameof(CurrentApiKey_GPT)));
        }
        public string? CurrentApiKey_Gemini
        {
            get => _currentApiKey_Gemini;
            set => SetProperty(ref _currentApiKey_Gemini, value, onChanged: () => OnPropertyChanged(nameof(CurrentApiKey_Gemini)));
        }
        #endregion

        #region API 키 로드
        private async void LoadGPTApiKey()
        {
            try
            {
                var apiKey = await SecureStorage.GetAsync("GPTApiKey");
                CurrentApiKey_GPT = apiKey ?? AppResources.error_api_set; // null일 경우 대체 텍스트
            }
            catch (Exception)
            {
                await Toast.Make(AppResources.error_api_load_failed, ToastDuration.Long).Show();
            }
        }

        private async void LoadGeminiApiKey()
        {
            try
            {
                var apiKey = await SecureStorage.GetAsync("GeminiApiKey");
                CurrentApiKey_Gemini = apiKey ?? AppResources.error_api_set; // null일 경우 대체 텍스트
            }
            catch (Exception)
            {
                await Toast.Make(AppResources.error_api_load_failed, ToastDuration.Long).Show();
            }
        }
        #endregion

        #region API 키 저장
        public async Task SaveGPTApiKey(string newApiKey)
        {
            try
            {
                await SecureStorage.SetAsync("GPTApiKey", newApiKey);
                LoadGPTApiKey();
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
            catch (Exception)
            {
                var updatePopup = new AlertPopup
                {
                    MainText = AppResources.error,
                    btn1Text = AppResources.ok
                };
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                }
            }
        }

        public async Task SaveGeminiApiKey(string newApiKey)
        {
            try
            {
                await SecureStorage.SetAsync("GeminiApiKey", newApiKey);
                LoadGeminiApiKey();
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
            catch (Exception)
            {
                var updatePopup = new AlertPopup
                {
                    MainText = AppResources.error,
                    btn1Text = AppResources.ok
                };
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                }
            }
        }
        #endregion

        #region API 키 삭제
        public async Task DeleteGPTApiKey()
        {
            try
            {
                SecureStorage.Remove("GPTApiKey");
                LoadGPTApiKey();
                var updatePopup = new AlertPopup
                {
                    MainText = AppResources.api_del_complete,
                    btn1Text = AppResources.ok
                };
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                }
            }
            catch (Exception)
            {
                var updatePopup = new AlertPopup
                {
                    MainText = AppResources.error,
                    btn1Text = AppResources.ok
                };
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                }
            }
        }

        public async Task DeleteGeminiApiKey()
        {
            try
            {
                SecureStorage.Remove("GeminiApiKey");
                LoadGeminiApiKey();
                var updatePopup = new AlertPopup
                {
                    MainText = AppResources.api_del_complete,
                    btn1Text = AppResources.ok
                };
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                }
            }
            catch (Exception)
            {
                var updatePopup = new AlertPopup
                {
                    MainText = AppResources.error,
                    btn1Text = AppResources.ok
                };
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                }
            }
        }
        #endregion
    }
}
