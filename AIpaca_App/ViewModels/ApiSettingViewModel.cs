using System.ComponentModel;
using System.Runtime.CompilerServices;
using AIpaca_App.Resources.Localization;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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
            LoadApiKey();
        }

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

        #region API 키 로드
        private void LoadApiKey()
        {
            LoadGPTApiKey();
            LoadGeminiApiKey();
        }

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
        public async void SaveGPTApiKey(string? newApiKey)
        {
            if (newApiKey != null)  // null 체크
            {
                try
                {
                    await SecureStorage.SetAsync("GPTApiKey", newApiKey);
                    LoadApiKey();  // Refresh the displayed key
                }
                catch (Exception)
                {
                    // 로그 기록 또는 사용자에게 피드백
                }
            }
        }

        public async void SaveGeminiApiKey(string? newApiKey)
        {
            if (newApiKey != null)  // null 체크
            {
                try
                {
                    await SecureStorage.SetAsync("GeminiApiKey", newApiKey);
                    LoadApiKey();  // Refresh the displayed key
                }
                catch (Exception)
                {
                    // 로그 기록 또는 사용자에게 피드백
                }
            }
        }
        #endregion

        #region API 키 삭제
        public async void DeleteGPTApiKey()
        {
            try
            {
                await SecureStorage.SetAsync("GPTApiKey", "");
                LoadApiKey();  // Refresh the displayed key
            }
            catch (Exception)
            {
                // 로그 기록 또는 사용자에게 피드백
            }
        }

        public async void DeleteGeminiApiKey()
        {
            try
            {
                await SecureStorage.SetAsync("GeminiApiKey", "");
                LoadApiKey();  // Refresh the displayed key
            }
            catch (Exception)
            {
                // 로그 기록 또는 사용자에게 피드백
            }
        }
        #endregion
    }
}
