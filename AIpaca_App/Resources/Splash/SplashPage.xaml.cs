using System;
using System.Threading.Tasks;
using AIpaca_App.Data;
using AIpaca_App.Resources.Localization;
using AIpaca_App.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
namespace AIpaca_App.Resources.Splash
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
            InitializeApp();
        }

        private async void InitializeApp()
        {
            try
            {
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
                        // ���� ���� �޽��� ǥ��
                        await DisplayAlert("���� ����", "���ͳ� ������ Ȯ�����ּ���.", "Ȯ��");
                        // �ʿ��� ��� �� ���� �Ǵ� �ٸ� ��ġ�� ���� �� �ֽ��ϴ�.
                    });
                }
            }
            catch (Exception)
            {
                await Toast.Make("����").Show();
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
            var (baseUrl, _, _, _, pingEndpoint) = ApiConfigManager.LoadApiConfig();
            var requestUri = $"{baseUrl}{pingEndpoint}";
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

            // ��õ� ��Ŀ����
            int retryCount = 0;
            const int maxRetries = 3;

            while (retryCount < maxRetries)
            {

                await Task.Delay(1000); // ��õ� �� 1�� ���
                try
                {
                    var response = await client.GetAsync(requestUri);
                    if (response.IsSuccessStatusCode) return true;
                }
                catch (TaskCanceledException)
                {
                    // Ÿ�Ӿƿ� �α� ������Ʈ
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        statusLabel.Text = $"���� ���� �õ��� {retryCount + 1}/{maxRetries}..";
                    });
                }

                retryCount++;
                await Task.Delay(1000); // ��õ� �� 1�� ���
            }
            // ��Ÿ ���� �α�
            MainThread.BeginInvokeOnMainThread(() =>
            {
                statusLabel.Text = "��Ʈ��ũ ����";
            });
            return false;  // ��� ��õ� ���� �� false ��ȯ
        }



        private async Task CheckAppVersionAndUpdateUI()
        {
            try
            {
                statusLabel.Text = $"�� ���� Ȯ����..";
                await Task.Delay(500);

                bool isLatestVersion = await CheckIfAppIsLatestVersionAsync();
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    if (isLatestVersion)
                    {
                        await Toast.Make("���� �ֽ� �����Դϴ�.").Show();
                    }
                    else
                    {
                        // ���ο� ������Ʈ �˾��� ǥ���մϴ�.
                        var updatePopup = new SplashPopup
                        {
                            MainText = AppResources.newupdate,
                            btn1Text = AppResources.update
                        };
                        updatePopup.btn1Clicked += async (sender, e) => await UpdatePopup_UpdateClickedAsync(sender, e);
                        if (Application.Current?.MainPage != null)
                        {
                            await Application.Current.MainPage.ShowPopupAsync(updatePopup);
                        }
                    }
                });
            }
            catch (Exception)
            {
                await Toast.Make("�� ���� Ȯ�� ����").Show();
            }
        }

        private async Task UpdatePopup_UpdateClickedAsync(object? sender, EventArgs e)
        {
            try
            {
                // ����ڰ� ������Ʈ�� ���� ��� �� ������ ���𷺼�
                var success = await Launcher.TryOpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.AIpaca&hl=en-US"));
                if (!success)
                {
                    // URL�� �� �� ���� ���, ����ڿ��� �߰����� �˸� ����
                    await Toast.Make("�� ���� �� �� �����ϴ�. �������� ������Ʈ�� Ȯ���� �ּ���.", ToastDuration.Long).Show();
                }
            }
            catch (Exception)
            {
                await Toast.Make("�����߻�").Show();
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
