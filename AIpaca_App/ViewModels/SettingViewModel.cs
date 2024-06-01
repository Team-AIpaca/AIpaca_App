using AIpaca_App.Data;
using AIpaca_App.Models;
using AIpaca_App.Resources.Localization;
using AIpaca_App.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AIpaca_App.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        private bool _isLoggedIn;
        private bool _isDarkModeEnabled;
        private string _appVersion;
        private string _loginEndpoint;
        private string _signupEndpoint;
        private DatabaseService databaseService;
        public int _pageSize;

        public string LastErrorMessage { get; private set; }

        public SettingViewModel()
        {
            databaseService = new DatabaseService();

            // 앱 설정에서 다크 모드 값을 불러와 프로퍼티에 설정합니다.
            IsDarkModeEnabled = Preferences.Get("IsDarkModeEnabled", false);

            // 앱 버전 가져오기
            _appVersion = AppInfo.VersionString;

            // ApiConfigManager.LoadApiConfig()에서 반환된 5개의 요소를 받기 위해 변수를 추가합니다.
            var (baseUrl, loginEndpoint, signupEndpoint, _, _, _, _, _, _, _) = ApiConfigManager.LoadApiConfig();
            _loginEndpoint = $"{baseUrl}{loginEndpoint}";
            _signupEndpoint = $"{baseUrl}{signupEndpoint}";

            // 에러 메시지 초기화
            LastErrorMessage = string.Empty;

            // 로그인 상태 초기화
            IsLoggedIn = false;  // 초기 로그인 상태를 false로 설정
        }

        #region 다크모드
        //다크모드
        public bool IsDarkModeEnabled
        {
            get => _isDarkModeEnabled;
            set
            {
                SetProperty(ref _isDarkModeEnabled, value);
                Preferences.Set("IsDarkModeEnabled", value);
                if (App.Current != null)
                {
                    App.Current.ApplyTheme(value);
                }
            }
        }

        #endregion

        #region 앱 버전
        //앱 버전
        public string AppVersion
        {
            get => _appVersion;
            set => SetProperty(ref _appVersion, value);
        }
        #endregion

        #region 서버 연결 확인

        public async Task CheckServerAsync()
        {
            try
            {
                await Task.Delay(300);

                var (baseUrl, _, _, _, _, pingEndpoint, _, _, _, _) = ApiConfigManager.LoadApiConfig();
                var requestUri = $"{baseUrl}{pingEndpoint}";
                var client = new HttpClient { Timeout = TimeSpan.FromSeconds(3) };

                var response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    var updatePopup = new AlertPopup
                    {
                        MainText = AppResources.success,
                        btn1Text = AppResources.ok
                    };
                    // 성공 시도 횟수를 ErrorLog에 기록
                    await databaseService.AddLogAsync(new Log
                    {
                        Message = $"{AppResources.splash_server_connect_success}",
                        Timestamp = DateTime.UtcNow,
                        Success = "Success"
                    });
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                    }
                }
                else
                {
                    var updatePopup = new AlertPopup
                    {
                        MainText = AppResources.success,
                        btn1Text = AppResources.ok
                    };
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                    }
                }
            }

            catch (HttpRequestException ex)
            {
                // 네트워크 문제 처리
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var updatePopup = new AlertPopup
                    {
                        MainText = AppResources.splash_server_connect_failed,
                        btn1Text = AppResources.ok
                    };
                    await databaseService.AddLogAsync(new Log
                    {
                        Message = $"{AppResources.splash_server_connect_failed}{ex.Message}",
                        Timestamp = DateTime.UtcNow,
                        Success = "Failed"
                    });
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                    }
                });
            }
            catch (TaskCanceledException ex)
            {
                // 타임아웃 문제 처리
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var updatePopup = new AlertPopup
                    {
                        MainText = AppResources.splash_server_connect_failed + ex,
                        btn1Text = AppResources.ok
                    };
                    await databaseService.AddLogAsync(new Log
                    {
                        Message = $"{AppResources.splash_server_connect_failed}{ex.Message}",
                        Timestamp = DateTime.UtcNow,
                        Success = "Failed"
                    });
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                    }
                });
            }
            catch (Exception ex)
            {
                // 일반 예외 처리
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var updatePopup = new AlertPopup
                    {
                        MainText = AppResources.splash_server_connect_failed + ex,
                        btn1Text = AppResources.ok
                    };
                    await databaseService.AddLogAsync(new Log
                    {
                        Message = $"{AppResources.splash_server_connect_failed}{ex.Message}",
                        Timestamp = DateTime.UtcNow,
                        Success = "Failed"
                    });
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                    }
                });
            }
        }

        #endregion

        #region 로그인
        //로그인 유지
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => SetProperty(ref _isLoggedIn, value);
        }

        // 로그인 로직
        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var loginData = new { username, password };
                var client = new HttpClient();
                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_loginEndpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    // 로그인 성공
                    IsLoggedIn = true;
                    await databaseService.AddLogAsync(new Log
                    {
                        Message = $"Login Success",
                        Timestamp = DateTime.UtcNow
                    });
                    return true;
                }
                else
                {
                    // 로그인 실패, 에러 메시지와 코드를 처리합니다.                    
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<ApiResponse>(responseContent);
                    LastErrorMessage = $"Error {responseObject?.StatusCode}: {responseObject?.message}";
                    await databaseService.AddLogAsync(new Log
                    {
                        Message = $"Login Failed : {LastErrorMessage}",
                        Timestamp = DateTime.UtcNow
                    });
                    return false;
                }
            }
            catch (Exception ex)
            {
                LastErrorMessage = $"Network Error: {ex.Message}";
                await databaseService.AddLogAsync(new Log
                {
                    Message = LastErrorMessage,
                    Timestamp = DateTime.UtcNow
                });
                return false;
            }
        }


        // 로그아웃 로직 예시
        public async void Logout()
        {
            IsLoggedIn = false;
            await databaseService.AddLogAsync(new Log
            {
                Message = $"Logout",
                Timestamp = DateTime.UtcNow
            });
        }
        #endregion

        #region 회원가입
        // 회원가입 기능
        public async Task<bool> SignupAsync(string email, string username, string password)
        {
            try
            {
                var signupData = new { email, username, password };
                var client = new HttpClient();
                var json = JsonSerializer.Serialize(signupData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await client.PostAsync(_signupEndpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    await Toast.Make("회원가입이 완료되었습니다.", ToastDuration.Long).Show();
                    return true;
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<ApiResponse>(responseContent);
                    LastErrorMessage = $"오류 {response.StatusCode}: {responseObject?.message ?? "알 수 없는 오류가 발생했습니다."}";
                    await Toast.Make(LastErrorMessage, ToastDuration.Long).Show();
                    return false;
                }
            }
            catch (Exception ex)
            {
                LastErrorMessage = $"네트워크 오류가 발생했습니다: {ex.Message}";
                await Toast.Make(LastErrorMessage, ToastDuration.Long).Show();
                return false;
            }
        }
        #endregion

        #region api 응답 모델
        private class ApiResponse
        {
            public int StatusCode { get; set; }
            public string? message { get; set; }
            public ResponseData? data { get; set; }
        }

        private class ResponseData
        {
            public string? RequestTime { get; set; }
            public EvaluationResult? result { get; set; }
        }

        private class EvaluationResult
        {
            public int Score { get; set; }
            public string? RecommandedTrans { get; set; }
            public string? Rating { get; set; }
        }
        #endregion
    }
}
