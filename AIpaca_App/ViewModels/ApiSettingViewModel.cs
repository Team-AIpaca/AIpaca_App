using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        private void LoadApiKey()
        {
            var apiKey = Preferences.Get("GeminiApiKey", "API 키가 설정되지 않았습니다.");
            CurrentApiKey = apiKey ?? "API 키가 설정되지 않았습니다."; // null일 경우 "API 키가 설정되지 않았습니다." 사용
        }

        public void SaveApiKey(string? newApiKey)
        {
            if (newApiKey != null)  // null 체크를 추가하여 null이 아닌 경우에만 저장합니다.
            {
                Preferences.Set("GeminiApiKey", newApiKey);
                LoadApiKey();  // Refresh the displayed key
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
