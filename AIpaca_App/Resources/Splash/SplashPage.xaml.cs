using System;
using System.Threading.Tasks;
using AIpaca_App.Data;
using AIpaca_App.Models;
using AIpaca_App.Resources.Localization;
using AIpaca_App.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace AIpaca_App.Resources.Splash
{
    public partial class SplashPage : ContentPage
    {
        private DatabaseService databaseService;

        public SplashPage()
        { 
            InitializeComponent();
            databaseService = new DatabaseService();
            InitializeApp();
        }

        private async void InitializeApp()
        {
            try
            {
                await Task.Delay(1000);
                statusLabel.Text = "앱 로딩중..";

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
                await Toast.Make("오류").Show();
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

        private async Task<bool> CheckInternetConnectionAsync()
        {
            await Task.Delay(500);
            var (baseUrl, _, _, _, pingEndpoint, _) = ApiConfigManager.LoadApiConfig();
            var requestUri = $"{baseUrl}{pingEndpoint}";
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

            // 재시도 메커니즘
            int retryCount = 0;
            const int maxRetries = 3;

            while (retryCount < maxRetries)
            {

                await Task.Delay(1000); // 재시도 전 1초 대기
                try
                {
                    var response = await client.GetAsync(requestUri);
                    if (response.IsSuccessStatusCode)
                    {
                        // 성공 시도 횟수를 ErrorLog에 기록
                        await databaseService.AddLogAsync(new Log
                        {
                            Message = $"인터넷 연결 성공 : 시도 횟수: {retryCount + 1}",
                            Timestamp = DateTime.UtcNow
                        });
                        return true;
                    }
                    
                }
                catch (TaskCanceledException)
                {
                    // 타임아웃 로그 업데이트
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        statusLabel.Text = $"서버 연결 시도중 {retryCount + 1}/{maxRetries}..";
                    });
                }

                retryCount++;
                await Task.Delay(1000); // 재시도 전 1초 대기
            }
            // 기타 예외 로그
            MainThread.BeginInvokeOnMainThread(() =>
            {
                statusLabel.Text = "네트워크 오류";
            });
            await databaseService.AddLogAsync(new Log
            {
                Message = $"인터넷 연결 실패",
                Timestamp = DateTime.UtcNow
            });
            return false;  // 모든 재시도 실패 시 false 반환
        }



        private async Task CheckAppVersionAndUpdateUI()
        {
            try
            {
                statusLabel.Text = $"앱 버전 확인중..";
                await Task.Delay(500);

                bool isLatestVersion = await CheckIfAppIsLatestVersionAsync();
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    if (isLatestVersion)
                    {
                        await Toast.Make("앱이 최신 버전입니다.").Show();
                    }
                    else
                    {
                        // 새로운 업데이트 팝업을 표시합니다.
                        var updatePopup = new AlertPopup
                        {
                            MainText = AppResources.newupdate,
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
                await Toast.Make("앱 버전 확인 오류").Show();
            }
        }

        private async Task SplashPopup_UpdateClickedAsync(object? sender, EventArgs e)
        {
            try
            {
                // 사용자가 업데이트를 원할 경우 앱 스토어로 리디렉션
                var success = await Launcher.TryOpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.AIpaca&hl=en-US"));
                if (!success)
                {
                    // URL을 열 수 없는 경우, 사용자에게 추가적인 알림 제공
                    await Toast.Make("앱 스토어를 열 수 없습니다. 수동으로 업데이트를 확인해 주세요.", ToastDuration.Long).Show();
                }
            }
            catch (Exception)
            {
                await Toast.Make("오류발생").Show();
            }
        }


        private async Task<bool> CheckIfAppIsLatestVersionAsync()
        {
            // 버전 확인 로직 구현
            var currentVersion = VersionTracking.CurrentVersion;
            var latestVersion = await GetLatestVersionFromDatabaseAsync();
            return currentVersion == latestVersion;
        }

        private Task<string> GetLatestVersionFromDatabaseAsync()
        {
            // 원격 서버로부터 최신 버전 정보를 비동기적으로 받아옵니다.
            return Task.FromResult("1.5.3");
        }
    }
}
