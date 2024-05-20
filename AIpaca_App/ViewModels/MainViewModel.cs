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
        private string _originalText = string.Empty;
        private string _translatedText = string.Empty;
        private string _originalLang = string.Empty;
        private string _translatedLang = string.Empty;
        private string _translationResult_Gemini = string.Empty;
        private string _translationResult_GPT = string.Empty;
        private DatabaseService databaseService;

        public string LastErrorMessage { get; private set; }
        public IAsyncRelayCommand EvaluateTranslationCommand { get; }

        public MainViewModel(DatabaseService dbService)
        {
            // 에러 메시지 초기화
            LastErrorMessage = string.Empty;

            EvaluateTranslationCommand = new AsyncRelayCommand(EvaluateTranslationWrapper);
            databaseService = dbService;
        }

        #region 값을 받아오는 메서드
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

        public string TranslationResult_GPT
        {
            get => _translationResult_GPT;
            set => SetProperty(ref _translationResult_GPT, value);
        }

        public string TranslationResult_Gemini
        {
            get => _translationResult_Gemini;
            set => SetProperty(ref _translationResult_Gemini, value);
        }
        #endregion

        #region Mainpage 평가받기 기능

        // 래퍼 메서드
        private async Task EvaluateTranslationWrapper()
        {
            // ViewModel의 속성을 사용하여 파라미터 값을 전달
            await EvaluateTranslation(OriginalText, TranslatedText, OriginalLang, TranslatedLang);
        }

        private async Task<bool> CheckApiKey(string userApiKey)
        {
            // userApiKey가 비어있는지 확인
            if (string.IsNullOrEmpty(userApiKey))
            {
                // API 키가 없다면 사용자에게 알림
                await Toast.Make(AppResources.error_no_api, ToastDuration.Long).Show();
                return false; // 메서드 종료
            }
            return true;
        }

        private async Task<bool> CheckText(string originalText, string translatedText)
        {
            // originalText가 비어있는지 확인
            if (string.IsNullOrEmpty(originalText))
            {
                // originalText가 비어있다면 사용자에게 알림
                await Toast.Make(AppResources.error_no_text1, ToastDuration.Long).Show();
                return false; // 메서드 종료
            }

            //vtranslatedText가 비어있는지 확인
            if (string.IsNullOrEmpty(translatedText))
            {
                // translatedText가 비어있다면 사용자에게 알림
                await Toast.Make(AppResources.error_no_text2, ToastDuration.Long).Show();
                return false; // 메서드 종료
            }
            return true;
        }


        public async Task EvaluateTranslation(string originalText, string translatedText, string originalLang, string translatedLang)
        {
            try
            {
                // 각 요소 값 확인
                if (!await CheckText(originalText, translatedText))
                {
                    return;
                }
                var (baseUrl, _, _, gptEndpoint, geminiEndpoint, _, _, _, _, _) = ApiConfigManager.LoadApiConfig();

                string requestUri_gpt = $"{baseUrl}{gptEndpoint}";
                string requestUri_gemini = $"{baseUrl}{geminiEndpoint}";

                // 두 작업을 병렬로 실행
                var gptTask = EvaluateTranslation_GPT(requestUri_gpt, originalText, translatedText, originalLang, translatedLang);
                var geminiTask = EvaluateTranslation_Gemini(requestUri_gemini, originalText, translatedText, originalLang, translatedLang);

                // 모든 작업이 완료될 때까지 기다림
                await Task.WhenAll(gptTask, geminiTask);
            }
            catch (Exception ex)
            {
                await Toast.Make($"{AppResources.error} : {ex.Message}", ToastDuration.Long).Show();
                throw;
            }
        }

        public async Task EvaluateTranslation_GPT(string requestUri, string originalText, string translatedText, string originalLang, string translatedLang)
        {
            try
            {
                string userApiKey = await SecureStorage.GetAsync("GPTApiKey") ?? string.Empty;
                if (!await CheckApiKey(userApiKey))
                {
                    TranslationResult_GPT = AppResources.error_no_api;
                    return;
                }

                TranslationResult_GPT = AppResources.loading;

                var requestData = new
                {
                    OpenAIAPIKey = userApiKey,
                    GPTVersion = "gpt-4-turbo",
                    Original = originalText,
                    OriginalLang = originalLang,
                    Translated = translatedText,
                    TranslatedLang = translatedLang,
                    EvaluationLang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName
                };

                var client = new HttpClient();
                var json = JsonSerializer.Serialize(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(requestUri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);

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

                    TranslationResult_GPT = $"{AppResources.score} : {result?.Score}\n{AppResources.recommend} : {result?.RecommandedTrans}\n{result?.Rating}";

                    await databaseService.AddRecordAsync(record);
                    await databaseService.AddLogAsync(new Log
                    {
                        Message = $"{AppResources.api_request_successful} : {record}",
                        Timestamp = DateTime.UtcNow,
                        Success = "Success"
                    });
                }
                else
                {
                    await Toast.Make(AppResources.api_request_failed, ToastDuration.Long).Show();

                    var errorMessage = $"{AppResources.error} : {apiResponse?.StatusCode} - {apiResponse?.message}";
                    TranslationResult_GPT = errorMessage;
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
                await Toast.Make($"{AppResources.error} : {ex.Message}", ToastDuration.Long).Show();
                TranslationResult_GPT = AppResources.error;
                await databaseService.AddLogAsync(new Log
                {
                    Message = $"{AppResources.api_request_failed}{ex.Message}",
                    Timestamp = DateTime.UtcNow,
                    Success = "Failed"
                });
            }
        }



        public async Task EvaluateTranslation_Gemini(string requestUri, string originalText, string translatedText, string originalLang, string translatedLang)
        {
            try
            {
                string userApiKey = await SecureStorage.GetAsync("GeminiApiKey") ?? string.Empty;
                if (!await CheckApiKey(userApiKey))
                {
                    TranslationResult_Gemini = AppResources.error_no_api;
                    return;
                }

                TranslationResult_Gemini = AppResources.loading;

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
                var response = await client.PostAsync(requestUri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);

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
                    TranslationResult_Gemini = $"{AppResources.score} : {result?.Score}\n{AppResources.recommend} : {result?.RecommandedTrans}\n{result?.Rating}";
                    await databaseService.AddRecordAsync(record);
                    await databaseService.AddLogAsync(new Log
                    {
                        Message = $"{AppResources.api_request_successful} : {record}",
                        Timestamp = DateTime.UtcNow,
                        Success = "Success"
                    });
                }
                else
                {
                    await Toast.Make(AppResources.api_request_failed, ToastDuration.Long).Show();

                    var errorMessage = $"{AppResources.error} : {apiResponse?.StatusCode} - {apiResponse?.message}";
                    TranslationResult_Gemini = errorMessage;
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
                await Toast.Make($"{AppResources.error} : {ex.Message}", ToastDuration.Long).Show();
                TranslationResult_Gemini = AppResources.error;
                await databaseService.AddLogAsync(new Log
                {
                    Message = $"{AppResources.api_request_failed}{ex.Message}",
                    Timestamp = DateTime.UtcNow,
                    Success = "Failed"
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

