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
                await Task.Delay(300);
                statusLabel.Text = "Loading..";

                // ���ͳ� ���� Ȯ��
                if (await CheckInternetConnectionAsync())
                {
                    // ���ͳ� ������ Ȯ�εǸ� �� ������ Ȯ���մϴ�.
                    await CheckAppVersionAndUpdateUI();
                    
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        // ���ο� ������Ʈ �˾��� ǥ���մϴ�.
                        var updatePopup = new AlertPopup
                        {
                            MainText = AppResources.error_connection,
                            btn1Text = AppResources.ok
                        };
                        if (Application.Current?.MainPage != null)
                        {
                            await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                        }
                        // �ʿ��� ��� �� ���� �Ǵ� �ٸ� ��ġ�� ���� �� �ֽ��ϴ�.
                    });
                }
            }
            catch (Exception)
            {
                await Toast.Make("Error").Show();
                // ���� �߻��� ApiConfig.xml Ȯ��
            }
            finally
            {
                // ���� �������� ��ȯ
                if (Application.Current != null)
                {
                    Application.Current.MainPage = new AppShell();
                }
            }
        }

        private async Task<bool> CheckInternetConnectionAsync()
        {
            await Task.Delay(300);
            var (baseUrl, _, _, _, pingEndpoint, _) = ApiConfigManager.LoadApiConfig();
            var requestUri = $"{baseUrl}{pingEndpoint}";
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

            // ��õ� ��Ŀ����
            int retryCount = 0;
            const int maxRetries = 3;

            while (retryCount < maxRetries)
            {

                await Task.Delay(500); // ��õ� ���
                try
                {
                    var response = await client.GetAsync(requestUri);
                    if (response.IsSuccessStatusCode)
                    {
                        // ���� �õ� Ƚ���� ErrorLog�� ���
                        await databaseService.AddLogAsync(new Log
                        {
                            Message = $"Server connection successful - Count : {retryCount + 1}",
                            Timestamp = DateTime.UtcNow
                        });
                        return true;
                    }
                }
                catch (TaskCanceledException)
                {
                    // Ÿ�Ӿƿ� �α� ������Ʈ
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        statusLabel.Text = $"Connecting to server - Count :  {retryCount + 1}/{maxRetries}..";
                    });
                }

                retryCount++;
                await Task.Delay(500); // ��õ� ���
            }
            // ��Ÿ ���� �α�
            MainThread.BeginInvokeOnMainThread(() =>
            {
                statusLabel.Text = "Server connection failed";
            });
            await databaseService.AddLogAsync(new Log
            {
                Message = $"{statusLabel.Text}",
                Timestamp = DateTime.UtcNow
            });
            return false;  // ��� ��õ� ���� �� false ��ȯ
        }



        private async Task CheckAppVersionAndUpdateUI()
        {
            try
            {
                statusLabel.Text = $"Checking app version..";
                await Task.Delay(300);

                bool isLatestVersion = await CheckIfAppIsLatestVersionAsync();
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    if (isLatestVersion)
                    {
                        await Toast.Make("It's the latest version").Show();
                    }
                    else
                    {
                        // ���ο� ������Ʈ �˾��� ǥ���մϴ�.
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
                await Toast.Make("App Version Verification Error").Show();
            }
        }

        private async Task SplashPopup_UpdateClickedAsync(object? sender, EventArgs e)
        {
            try
            {
                // ����ڰ� ������Ʈ�� ���� ��� �� ������ ���𷺼�
                var success = await Launcher.TryOpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.AIpaca&hl=en-US"));
                if (!success)
                {
                    // URL�� �� �� ���� ���, ����ڿ��� �߰����� �˸� ����
                    await Toast.Make("App Store could not be opened, please check manually for updates.", ToastDuration.Long).Show();
                }
            }
            catch (Exception)
            {
                await Toast.Make("Error").Show();
            }
        }


        private async Task<bool> CheckIfAppIsLatestVersionAsync()
        {
            // ���� Ȯ�� ���� ����
            var currentVersion = VersionTracking.CurrentVersion;
            var latestVersion = await GetLatestVersionFromDatabaseAsync();
            return currentVersion == latestVersion;
        }

        private Task<string> GetLatestVersionFromDatabaseAsync()
        {
            // ���� �����κ��� �ֽ� ���� ������ �񵿱������� �޾ƿɴϴ�.
            return Task.FromResult("1.5.3");
        }
    }
}
