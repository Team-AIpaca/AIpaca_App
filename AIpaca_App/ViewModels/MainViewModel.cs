using AIpaca_App.Data;
using AIpaca_App.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using static AIpaca_App.Views.Account.SignupPagePopup;

namespace AIpaca_App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isLoggedIn;
        private bool _isDarkModeEnabled;
        private string _appVersion;
        private string _loginEndpoint;
        private string _signupEndpoint;
        private string _originalText = string.Empty;
        private string _translatedText = string.Empty;
        private string _originalLang = string.Empty;
        private string _translatedLang = string.Empty;
        private string _translationResult = string.Empty;
        private DatabaseService databaseService;

        public string LastErrorMessage { get; private set; }
        public IAsyncRelayCommand EvaluateTranslationCommand { get; }

        public MainViewModel(DatabaseService dbService)
        {
            // 앱 설정에서 다크 모드 값을 불러와 프로퍼티에 설정합니다.
            IsDarkModeEnabled = Preferences.Get("IsDarkModeEnabled", false);
            // 앱 버전 가져오기
            _appVersion = AppInfo.VersionString;

            // ApiConfigManager.LoadApiConfig()에서 반환된 5개의 요소를 받기 위해 변수를 추가합니다.
            var (baseUrl, loginEndpoint, signupEndpoint, _, _, _) = ApiConfigManager.LoadApiConfig();
            _loginEndpoint = $"{baseUrl}{loginEndpoint}";
            _signupEndpoint = $"{baseUrl}{signupEndpoint}";

            // 에러 메시지 초기화
            LastErrorMessage = string.Empty;

            // 로그인 상태 초기화
            IsLoggedIn = false;  // 초기 로그인 상태를 false로 설정

            EvaluateTranslationCommand = new AsyncRelayCommand(EvaluateTranslationWrapper);
            databaseService = dbService;
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
            var signupData = new { email, username, password };
            var client = new HttpClient();
            var json = JsonSerializer.Serialize(signupData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
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

        #region Mainpage 평가받기 기능

        public void SetOriginalLang(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    OriginalLang = "ko"; // 한국어
                    break;
                case 1:
                    OriginalLang = "en"; // 영어
                    break;
                case 2:
                    OriginalLang = "ja"; // 일본어
                    break;
            }
        }
        
        public void SetTranslatedLang(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    TranslatedLang = "ko"; // 한국어
                    break;
                case 1:
                    TranslatedLang = "en"; // 영어
                    break;
                case 2:
                    TranslatedLang = "ja"; // 일본어
                    break;
            }
        }

        public string OriginalText
        {
            get => _originalText;
            set => SetProperty(ref _originalText, value);
        }

        public string TranslatedText
        {
            get => _translatedText;
            set => SetProperty(ref _translatedText, value);
        }

        public string OriginalLang
        {
            get => _originalLang;
            set => SetProperty(ref _originalLang, value);
        }

        public string TranslatedLang
        {
            get => _translatedLang;
            set => SetProperty(ref _translatedLang, value);
        }

        public string TranslationResult
        {
            get => _translationResult;
            set => SetProperty(ref _translationResult, value);
        }


        // 래퍼 메서드
        private async Task EvaluateTranslationWrapper()
        {
            // ViewModel의 속성을 사용하여 파라미터 값을 전달
            await EvaluateTranslation(OriginalText, TranslatedText, OriginalLang, TranslatedLang);
        }


        public async Task EvaluateTranslation(string originalText, string translatedText, string originalLang, string translatedLang)
        {
            var (baseUrl, _, _, geminiEndpoint, _, _) = ApiConfigManager.LoadApiConfig();

            // 사용자가 입력한 API 키를 Preferences에서 불러옵니다.
            var userApiKey = await SecureStorage.GetAsync("GeminiApiKey");

            // userApiKey가 비어있는지 확인
            if (string.IsNullOrEmpty(userApiKey))
            {
                // API 키가 없다면 사용자에게 알림
                await Toast.Make(Resources.Localization.AppResources.error_no_api, ToastDuration.Long).Show();
                return; // 메서드 종료
            }

            // originalText가 비어있는지 확인
            if (string.IsNullOrEmpty(originalText))
            {
                // originalText가 비어있다면 사용자에게 알림
                await Toast.Make(Resources.Localization.AppResources.error_no_text1, ToastDuration.Long).Show();
                return; // 메서드 종료
            }

            //vtranslatedText가 비어있는지 확인
            if (string.IsNullOrEmpty(translatedText))
            {
                // translatedText가 비어있다면 사용자에게 알림
                await Toast.Make(Resources.Localization.AppResources.error_no_text2, ToastDuration.Long).Show();
                return; // 메서드 종료
            }

            var requestUri = $"{baseUrl}{geminiEndpoint}";

            var requestData = new
            {
                GeminiAPIKey = userApiKey,
                Original = originalText,
                OriginalLang = originalLang,
                Translated = translatedText,
                TranslatedLang = translatedLang,
                EvaluationLang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName  
            };

            var client = new HttpClient();
            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                TranslationResult = $"로딩중";
                var response = await client.PostAsync(requestUri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);
                if (response.IsSuccessStatusCode)
                {
                    var score = Resources.Localization.AppResources.score;
                    var recommend = Resources.Localization.AppResources.recommend;
                    if (apiResponse?.StatusCode == 200 && apiResponse.data?.result != null)
                    {
                        var result = apiResponse.data.result;
                        var record = new EvRecord
                        {
                            OriginalText = " " + originalText,
                            OriginalLang = originalLang,
                            TranslatedText = " " + translatedText,
                            TranslatedLang = translatedLang,
                            Message = apiResponse.message ?? "No message",
                            RequestTime = apiResponse.data?.RequestTime ?? "No timestamp",
                            Score = result.Score,
                            RecommendedTrans = result.RecommandedTrans ?? "No recommendation",
                            Rating = result.Rating ?? "No rating"
                        };
                        TranslationResult = $"{score} : {result?.Score}\n{recommend}: {result?.RecommandedTrans}\n{result?.Rating}";
                        // 데이터베이스에 레코드 저장
                        await databaseService.AddRecordAsync(record);
                        await databaseService.AddLogAsync(new Log
                        {
                            Message = $"API Request Successful : {record}",
                            Timestamp = DateTime.UtcNow,
                            Success = "Success"
                        });
                    }
                    else
                    {
                        await Toast.Make("API 응답이 유효하지 않습니다.", ToastDuration.Long).Show();
                    }
                }
                else
                {
                    var errorMessage = $"오류 발생: {apiResponse?.StatusCode} - {apiResponse?.message}";
                    //await Toast.Make(errorMessage, ToastDuration.Long).Show();
                    TranslationResult = errorMessage;
                    await databaseService.AddLogAsync(new Log
                    {
                        Message = errorMessage,
                        Timestamp = DateTime.UtcNow,
                        Success = "Failed"
                    });
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"네트워크 오류가 발생했습니다: {ex.Message}", ToastDuration.Long).Show();
                TranslationResult = " 네트워크 오류가 발생했습니다\n 다시 시도해주세요";
                await databaseService.AddLogAsync(new Log
                {
                    Message = $"API Request Failed : {ex.Message}",
                    Timestamp = DateTime.UtcNow,
                    Success = "Failed"
                });
            }
        }

        // 받아온 api 요청을 db에 저장

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
 
