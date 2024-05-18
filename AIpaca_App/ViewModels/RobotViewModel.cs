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
    public class RobotViewModel : BaseViewModel
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

        #region 번역 기능
        public string OriginalTransText
        {
            get => _originalTransText;
            set => SetProperty(ref _originalTransText, value);
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

        public string GoogleTranslationResult
        {
            get => _GoogleTranslationResult;
            set => SetProperty(ref _GoogleTranslationResult, value);
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

        private async Task<bool> CheckText(string originalText)
        {
            // originalText가 비어있는지 확인
            if (string.IsNullOrEmpty(originalText))
            {
                // originalText가 비어있다면 사용자에게 알림
                await Toast.Make(AppResources.error_no_text1, ToastDuration.Long).Show();
                return false; // 메서드 종료
            }
            return true;
        }

        public async Task Translation(string originalText, string originalLang, string translatedLang)
        {
            // 각 요소 값 확인
            if (!await CheckText(originalText))
            {
                await Toast.Make(AppResources.error_no_text1, ToastDuration.Long).Show();
                return;
            }
            var (baseUrl, _, _, _, _, _, googletrans, papago, deepltrans, libretrans) = ApiConfigManager.LoadApiConfig();
            var requestUri_google = $"{baseUrl}{googletrans}";
            var requestUri_papago = $"{baseUrl}{papago}";
            var requestUri_deepl = $"{baseUrl}{deepltrans}";
            var requestUri_Libre = $"{baseUrl}{libretrans}";
            

            var googleTask = Translation_Google(requestUri_google, originalText, originalLang, translatedLang);
            var papagoTask = Translation_Papago(requestUri_papago, originalText, originalLang, translatedLang);
            var deeplTask = Translation_DeepL(requestUri_deepl, originalText, originalLang, translatedLang);
            var libreTask = Translation_Libre(requestUri_Libre, originalText, originalLang, translatedLang);

            // 모든 작업이 완료될 때까지 기다림
            await Task.WhenAll(googleTask, papagoTask, deeplTask, libreTask);

        }

        public async Task Translation_Google(string requestUri, string originalText, string originalLang, string translatedLang)
        {
            try
            {
                GoogleTranslationResult = AppResources.loading;

                var requestData = new
                {
                    text = originalText,
                    OriginalLang = originalLang,
                    TranslatedLang = translatedLang
                };

                var client = new HttpClient();
                var json = JsonSerializer.Serialize(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(requestUri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);

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
                    var errorMessage = AppResources.error + $" : {apiResponse?.StatusCode} - {apiResponse?.message}";
                    GoogleTranslationResult = errorMessage;
                    await Toast.Make(GoogleTranslationResult, ToastDuration.Long).Show();
                    await databaseService.AddLogAsync(new Log
                    {
                        Message = errorMessage,
                        Timestamp = DateTime.UtcNow,
                        Success = "false"
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
                    Timestamp = DateTime.UtcNow,
                    Success = "false"
                });
            }
        }

        public async Task Translation_Papago(string requestUri, string originalText, string originalLang, string translatedLang)
        {
            await Toast.Make("Papago 번역기능은 준비중입니다.", ToastDuration.Long).Show();
        }

        public async Task Translation_DeepL(string requestUri, string originalText, string originalLang, string translatedLang)
        {
            await Toast.Make("DeepL 번역기능은 준비중입니다.", ToastDuration.Long).Show();
        }

        public async Task Translation_Libre(string requestUri, string originalText, string originalLang, string translatedLang)
        {
            await Toast.Make("LibreTranslate 번역기능은 준비중입니다.", ToastDuration.Long).Show();
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
