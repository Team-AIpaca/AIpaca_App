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
            await Task.Delay(300);

            await CheckDB_EvRecord();

            await Task.Delay(300);

            try
            {
                await Task.Delay(300);
                statusLabel.Text = AppResources.loading;

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
                await Toast.Make(AppResources.error).Show();
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

        // db �ʱ���� Ȯ��
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
                    RequestTime = DateTime.Now.ToString() ?? "No timestamp",
                    Score = 100,
                    RecommendedTrans = " Sample : Recommended Trans by AI" ?? "No recommendation",
                    Rating = " Sample : Rating by AI" ?? "No rating"
                };

                await _databaseService.AddRecordAsync(record);
            }
        }

        // ���ͳ� ���� Ȯ��
        private async Task<bool> CheckInternetConnectionAsync()
        {
            try
            {
                await Task.Delay(300);
                var (baseUrl, _, _, _, _, pingEndpoint, _, _, _, _) = ApiConfigManager.LoadApiConfig();
                var requestUri = $"{baseUrl}{pingEndpoint}";
                var client = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

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
                        // Ÿ�Ӿƿ� �α� ������Ʈ
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            statusLabel.Text = AppResources.splash_server_try_connect + $": {retryCount + 1} / {maxRetries}";
                        });
                    }

                    retryCount++;
                    await Task.Delay(500); // ��õ� ���
                }
                // ��Ÿ ���� �α�
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    statusLabel.Text = AppResources.splash_server_connect_failed;
                });
                await _databaseService.AddLogAsync(new Log
                {
                    Message = statusLabel.Text,
                    Timestamp = DateTime.Now
                });
                return false;  // ��� ��õ� ���� �� false ��ȯ
            }
            catch (Exception)
            {
                // ��Ÿ ���� �α�
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
        
        // �� ���� Ȯ��
        private async Task CheckAppVersionAndUpdateUI()
        {
            try
            {
                statusLabel.Text = AppResources.splash_check_version;
                await Task.Delay(300);

                bool isLatestVersion = await CheckIfAppIsLatestVersionAsync();
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    if (isLatestVersion)
                    {
                        await Toast.Make(AppResources.splash_version_latest).Show();
                    }
                    else
                    {
                        // ���ο� ������Ʈ �˾��� ǥ���մϴ�.
                        var updatePopup = new AlertPopup
                        {
                            MainText = AppResources.newupdate + "\n(������Ʈ�� ��� �׽�Ʈ�� ���� ��µ˴ϴ�)",
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

        // ������Ʈ ��ư Ŭ���� ������̵�
        private async Task SplashPopup_UpdateClickedAsync(object? sender, EventArgs e)
        {
            try
            {
                // ����ڰ� ������Ʈ�� ���� ��� �� ������ ���𷺼�
                var success = await Launcher.TryOpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.AIpaca&hl=en-US"));
                if (!success)
                {
                    // URL�� �� �� ���� ���, ����ڿ��� �߰����� �˸� ����
                    await Toast.Make(AppResources.splash_error_appstore, ToastDuration.Long).Show();
                }
            }
            catch (Exception)
            {
                await Toast.Make(AppResources.error).Show();
            }
        }

        // �� ���� Ȯ��
        private async Task<bool> CheckIfAppIsLatestVersionAsync()
        {
            // ���� Ȯ�� ���� ����
            var currentVersion = VersionTracking.CurrentVersion;
            var latestVersion = await GetLatestVersionFromDatabaseAsync();
            return currentVersion == latestVersion;
        }

        // �� ���� üũ
        private Task<string> GetLatestVersionFromDatabaseAsync()
        {
            // ���� �����κ��� �ֽ� ���� ������ �񵿱������� �޾ƿɴϴ�.
            // ���÷��� ȭ�� �˶��� ���� ���� �ۼ��Ǿ����ϴ�.
            return Task.FromResult("1.5.3");
        }
    }
}
