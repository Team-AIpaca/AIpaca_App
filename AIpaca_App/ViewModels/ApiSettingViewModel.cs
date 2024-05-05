﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using AIpaca_App.Resources.Localization;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Storage;

namespace AIpaca_App.ViewModels
{
    public class ApiSettingViewModel : INotifyPropertyChanged
    {
        private string? _currentApiKey;

        public string? CurrentApiKey
        {
            get => _currentApiKey;
            set
            {
                if (_currentApiKey != value)
                {
                    _currentApiKey = value;
                    OnPropertyChanged();
                }
            }
        }

        public ApiSettingViewModel()
        {
            LoadApiKey();
        }

        private async void LoadApiKey()
        {
            try
            {
                var apiKey = await SecureStorage.GetAsync("GeminiApiKey");
                CurrentApiKey = apiKey ?? AppResources.error_api_set; // null일 경우 대체 텍스트
            }
            catch (Exception)
            {
                CurrentApiKey = AppResources.error_api_load_failed;
                await Toast.Make($"{CurrentApiKey}", ToastDuration.Long).Show();
            }
        }

        public async void SaveApiKey(string? newApiKey)
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
