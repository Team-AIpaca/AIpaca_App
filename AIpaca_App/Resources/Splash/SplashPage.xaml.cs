using System;
using System.Threading.Tasks;
using AIpaca_App.Data;
using AIpaca_App.Views;
using CommunityToolkit.Maui.Alerts;
using Microsoft.Maui.Controls;

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
                    CheckAppVersionAndUpdateUI();

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



        private async void CheckAppVersionAndUpdateUI()
        {
            try
            {
                statusLabel.Text = $"�� ���� Ȯ����..";
                await Task.Delay(500); // 0.5�� ���

                bool isLatestVersion = await CheckIfAppIsLatestVersionAsync();

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    if (isLatestVersion)
                    {
                        // �ֽ� ������ ��� ������ �佺Ʈ �޽����� �˸�
                        await Toast.Make("���� �ֽ� �����Դϴ�.").Show();
                    }
                    else
                    {
                        // ������Ʈ �ʿ��� ��� �˸�â���� �˸�
                        var action = await DisplayAlert("���� Ȯ��", "���ο� ������ ���� �ֽ��ϴ�. ������Ʈ�� �ʿ��մϴ�.", "������Ʈ", "���");
                        if (action)
                        {
                            // ����ڰ� ������Ʈ�� ���� ��� �� ������ ���𷺼�
                            var success = await Launcher.TryOpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.AIpaca&hl=en-US"));
                            if (!success)
                            {
                                // URL�� �� �� ���� ���, ����ڿ��� �߰����� �˸� ����
                                await DisplayAlert("����", "�� ���� �� �� �����ϴ�. �������� ������Ʈ�� Ȯ���� �ּ���.", "Ȯ��");
                            }
                        }

                    }
                });
            }
            catch (Exception ex)
            {
                // ���� ó��
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("����", $"�� ������ Ȯ���ϴ� ���� ������ �߻��߽��ϴ�: {ex.Message}", "Ȯ��");
                });
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
