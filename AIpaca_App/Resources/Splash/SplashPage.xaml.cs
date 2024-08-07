using System;
using System.Threading.Tasks;
using AIpaca_App.Data;
using AIpaca_App.Models;
using AIpaca_App.Resources.Localization;
using AIpaca_App.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using static SQLite.SQLite3;

namespace AIpaca_App.Resources.Splash
{
    public partial class SplashPage : ContentPage
    {
        private DatabaseService _databaseService;

        public SplashPage()
        { 
            InitializeComponent();
            _databaseService = new DatabaseService();
            InitializeApp();
        }

        private async void InitializeApp()
        {
            statusLabel.Text = AppResources.cheak_DB;
            await Task.Delay(100);

            await CheckDB_EvRecord();

            await Task.Delay(100);

            try
            {
                await Task.Delay(100);
                statusLabel.Text = AppResources.loading;

                // 인터넷 연결 확인
                if (await CheckInternetConnectionAsync())
                {
                    // 인터넷 연결이 확인되면 앱 버전을 확인합니다.
                    await CheckAppVersionAndUpdateUI();
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        // 새로운 업데이트 팝업을 표시합니다.
                        var updatePopup = new AlertPopup
                        {
                            MainText = AppResources.error_connection,
                            btn1Text = AppResources.ok
                        };
                        if (Application.Current?.MainPage != null)
                        {
                            await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                        }
                        // 필요한 경우 앱 종료 또는 다른 조치를 취할 수 있습니다.
                    });
                }
            }
            catch (Exception)
            {
                await Toast.Make(AppResources.error).Show();
                // 오류 발생시 ApiConfig.xml 확인
            }
            finally
            {
                // 메인 페이지로 전환
                if (Application.Current != null)
                {
                    Application.Current.MainPage = new AppShell();
                }
            }
        }

        // db 초기상태 확인
        private async Task CheckDB_EvRecord()
        {
            var records = await _databaseService.GetAllRecordsAsync();
            if (records.Count == 0)
            {
                var record = new EvRecord
                {
                    OriginalText = "Sample : Original Text",
                    OriginalLang = "ko",
                    TranslatedText = " Sample : Translated Text ",
                    TranslatedLang = "en",
                    Message =" Sample : AI name" ?? "No message",
                    //RequestTime = DateTime.Now.ToString() ?? "No timestamp",
                    RequestTime = "2000-01-01" ?? "No timestamp",
                    Score = 100,
                    RecommendedTrans = " Sample : Recommended Trans by AI" ?? "No recommendation",
                    Rating = " Sample : Rating by AI" ?? "No rating"
                };

                await _databaseService.AddRecordAsync(record);
            }
        }

        // 인터넷 연결 확인
        private async Task<bool> CheckInternetConnectionAsync()
        {
            try
            {
                await Task.Delay(100);
                var (baseUrl, _, _, _, _, pingEndpoint, _, _, _, _) = ApiConfigManager.LoadApiConfig();
                var requestUri = $"{baseUrl}{pingEndpoint}";
                var client = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

                // 재시도 메커니즘
                int retryCount = 0;
                const int maxRetries = 3;

                while (retryCount < maxRetries)
                {

                    await Task.Delay(500); // 재시도 대기
                    try
                    {
                        var response = await client.GetAsync(requestUri);
                        if (response.IsSuccessStatusCode)
                        {
                            // 성공 시도 횟수를 ErrorLog에 기록
                            await _databaseService.AddLogAsync(new Log
                            {
                                Message = AppResources.splash_server_connect_success + $" : {retryCount + 1}",
                                Timestamp = DateTime.Now
                            });
                            return true;
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        // 타임아웃 로그 업데이트
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            statusLabel.Text = AppResources.splash_server_try_connect + $": {retryCount + 1} / {maxRetries}";
                        });
                    }

                    retryCount++;
                    await Task.Delay(300); // 재시도 대기
                }
                // 기타 예외 로그
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    statusLabel.Text = AppResources.splash_server_connect_failed;
                });
                await _databaseService.AddLogAsync(new Log
                {
                    Message = statusLabel.Text,
                    Timestamp = DateTime.Now
                });
                return false;  // 모든 재시도 실패 시 false 반환
            }
            catch (Exception)
            {
                // 기타 예외 로그
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    statusLabel.Text = AppResources.splash_server_connect_failed;
                });
                await _databaseService.AddLogAsync(new Log
                {
                    Message = statusLabel.Text,
                    Timestamp = DateTime.Now
                });
                return false;
                throw;
            }
        }
        
        // 앱 버전 확인
        private async Task CheckAppVersionAndUpdateUI()
        {
            try
            {
                statusLabel.Text = AppResources.splash_check_version;
                await Task.Delay(100);

                bool isLatestVersion = await CheckIfAppIsLatestVersionAsync();
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    if (isLatestVersion)
                    {
                        await Toast.Make(AppResources.splash_version_latest).Show();
                    }
                    else
                    {
                        // 새로운 업데이트 팝업을 표시합니다.
                        var updatePopup = new AlertPopup
                        {
                            MainText = AppResources.newupdate + "\n(업데이트가 없어도 테스트를 위해 출력됩니다)",
                            btn1Text = AppResources.update
                        };
                        updatePopup.btn1Clicked += async (sender, e) => await SplashPopup_UpdateClickedAsync(sender, e);
                        if (Application.Current?.MainPage != null)
                        {
                            await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                        }
                    }
                });
            }
            catch (Exception)
            {
                await Toast.Make(AppResources.splash_error_appversion).Show();
            }
        }

        // 업데이트 버튼 클릭시 스토어이동
        private async Task SplashPopup_UpdateClickedAsync(object? sender, EventArgs e)
        {
            try
            {
                // 사용자가 업데이트를 원할 경우 앱 스토어로 리디렉션
                var success = await Launcher.TryOpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.AIpaca&hl=en-US"));
                if (!success)
                {
                    // URL을 열 수 없는 경우, 사용자에게 추가적인 알림 제공
                    await Toast.Make(AppResources.splash_error_appstore, ToastDuration.Long).Show();
                }
            }
            catch (Exception)
            {
                await Toast.Make(AppResources.error).Show();
            }
        }

        // 앱 버전 확인
        private async Task<bool> CheckIfAppIsLatestVersionAsync()
        {
            // 버전 확인 로직 구현
            var currentVersion = VersionTracking.CurrentVersion;
            var latestVersion = await GetLatestVersionFromDatabaseAsync();
            return currentVersion == latestVersion;
        }

        // 앱 버전 체크
        private Task<string> GetLatestVersionFromDatabaseAsync()
        {
            // 원격 서버로부터 최신 버전 정보를 비동기적으로 받아옵니다.
            // 스플래쉬 화면 알람을 위해 임의 작성되었습니다.
            return Task.FromResult("1.5.3");
        }
    }
}
