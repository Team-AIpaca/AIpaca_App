using AIpaca_App.Data;
using AIpaca_App.Models;
using AIpaca_App.Resources.Localization;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIpaca_App.ViewModels
{
    public class RobotViewModel : INotifyPropertyChanged
    {

        private string _originalTransText = string.Empty;
        private string _originalLang = string.Empty;
        private string _translatedLang = string.Empty;
        private string _GoogleTranslationResult = string.Empty;
        private DatabaseService databaseService;
        public IAsyncRelayCommand TranslationCommand { get; }

        public RobotViewModel()
        {
            TranslationCommand = new AsyncRelayCommand(TranslationWrapper);
            databaseService = new DatabaseService();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region 번역 기능
        public string OriginalTransText
        {
            get => _originalTransText;
            set
            {
                _originalTransText = value;
                OnPropertyChanged(nameof(OriginalTransText));
            }
        }

        public string OriginalLang
        {
            get => _originalLang;
            set
            {
                _originalLang = value;
                OnPropertyChanged(nameof(OriginalLang));
            }
        }

        public string TranslatedLang
        {
            get => _translatedLang;
            set
            {
                _translatedLang = value;
                OnPropertyChanged(nameof(TranslatedLang));
            }
        }

        public string GoogleTranslationResult
        {
            get => _GoogleTranslationResult;
            set
            {
                _GoogleTranslationResult = value;
                OnPropertyChanged(nameof(GoogleTranslationResult));
            }
        }

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



        private async Task TranslationWrapper()
        {
            // ViewModel의 속성을 사용하여 파라미터 값을 전달
            await Translation(OriginalTransText, OriginalLang, TranslatedLang);
        }


        public async Task Translation(string originalText, string originalLang, string translatedLang)
        {
            var (baseUrl, _, _, _, _, googletrans) = ApiConfigManager.LoadApiConfig();


            //var userApiKey = await SecureStorage.GetAsync("GeminiApiKey");

            // originalText가 비어있는지 확인
            if (string.IsNullOrEmpty(originalText))
            {
                // originalText가 비어있다면 사용자에게 알림
                await Toast.Make(Resources.Localization.AppResources.error_no_text1, ToastDuration.Long).Show();
                return; // 메서드 종료
            }

            var requestUri = $"{baseUrl}{googletrans}";

            var requestData = new
            {
                text = originalText,
                OriginalLang = originalLang,
                TranslatedLang = translatedLang
            };

            var client = new HttpClient();
            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                GoogleTranslationResult = AppResources.loading;
                var response = await client.PostAsync(requestUri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);
                if (response.IsSuccessStatusCode)
                {
                    if (apiResponse?.StatusCode == 200 && apiResponse.data?.result != null)
                    {
                        var translatedText = apiResponse.data.result.Translation ?? AppResources.error;
                        var trecord = new TransRecord
                        {
                            OriginalText = originalText,
                            OriginalLang = originalLang,
                            TranslatedLang = translatedLang,
                            TranslatedText = translatedText,
                        };

                        //번역 출력
                        GoogleTranslationResult = translatedText;

                        // 데이터베이스에 레코드 저장
                        await databaseService.AddTransAsync(trecord);

                        //로그 저장
                        await databaseService.AddLogAsync(new Log
                        {
                            Message = AppResources.api_request_successful + $" : {trecord}",
                            Timestamp = DateTime.UtcNow
                        });
                    }
                    else
                    {
                        GoogleTranslationResult = AppResources.error_api_response;
                        await Toast.Make(GoogleTranslationResult, ToastDuration.Long).Show();
                    }
                }
                else
                {
                    var errorMessage = AppResources.error + $" : {apiResponse?.StatusCode} - {apiResponse?.message}";
                    //await Toast.Make(errorMessage, ToastDuration.Long).Show();
                    GoogleTranslationResult = errorMessage;
                    await databaseService.AddLogAsync(new Log
                    {
                        Message = errorMessage,
                        Timestamp = DateTime.UtcNow
                    });
                }
            }
            catch (Exception ex)
            {
                await Toast.Make(AppResources.error + $" : {ex.Message}", ToastDuration.Long).Show();
                GoogleTranslationResult = AppResources.error_network;
                //db에 로그 저장
                await databaseService.AddLogAsync(new Log
                {
                    Message = AppResources.api_request_failed + $" : {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
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
            public TransResult? result { get; set; }
        }

        private class TransResult
        {
            public string? Translation { get; set; }
        }
        #endregion
    }


}
