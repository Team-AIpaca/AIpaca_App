using AIpaca_App.Data;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static AIpaca_App.Views.Account.SignupPagePopup;

namespace AIpaca_App.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _isLoggedIn;
        private bool _isDarkModeEnabled;
        private string _appVersion;
        private string _loginEndpoint;

        public MainViewModel()
        {
            // 앱 설정에서 다크 모드 값을 불러와 프로퍼티에 설정합니다.
            IsDarkModeEnabled = Preferences.Get("IsDarkModeEnabled", false);
            // 앱 버전 가져오기
            _appVersion = AppInfo.VersionString;

            var (baseUrl, loginEndpoint, _) = ApiConfigManager.LoadApiConfig();
            _loginEndpoint = $"{baseUrl}{loginEndpoint}";
            
            // 에러 메시지 초기화
            LastErrorMessage = string.Empty;
            // 로그인 상태 초기화
            IsLoggedIn = false;  // 초기 로그인 상태를 false로 설정

        }

        //다크모드
        public bool IsDarkModeEnabled
        {
            get => _isDarkModeEnabled;
            set
            {
                _isDarkModeEnabled = value;
                OnPropertyChanged(nameof(IsDarkModeEnabled));
                // 다크 모드 설정값을 앱 설정에 저장합니다.
                Preferences.Set("IsDarkModeEnabled", value);
                if (App.Current != null)
                {
                    App.Current.ApplyTheme(value);
                }
            }
        }

        //앱 버전
        public string AppVersion
        {
            get => _appVersion;
            set
            {
                _appVersion = value;
                OnPropertyChanged(nameof(AppVersion));
            }
        }

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                if (_isLoggedIn != value)
                {
                    _isLoggedIn = value;
                    OnPropertyChanged(nameof(IsLoggedIn));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        // 로그인 로직
        public async Task<bool> LoginAsync(string username, string password)
        {
            var loginData = new { username, password };
            var client = new HttpClient();
            var json = JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(_loginEndpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    // 로그인 성공
                    IsLoggedIn = true;
                    return true;
                }
                else
                {
                    // 로그인 실패, 에러 메시지와 코드를 처리합니다.
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<ApiResponse>(responseContent);
                    LastErrorMessage = $"Error {responseObject?.StatusCode}: {responseObject?.message}";
                    return false;
                }
            }
            catch (Exception ex)
            {
                LastErrorMessage = $"Network error: {ex.Message}";
                return false;
            }
        }

        public string LastErrorMessage { get; private set; }

        private class ApiResponse
        {
            public int StatusCode { get; set; }
            public string? message { get; set; }
            public object? data { get; set; }
        }

        // 로그아웃 로직 예시
        public void Logout()
        {
            IsLoggedIn = false;
        }
    }
}
