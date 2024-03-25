using System;
using System.Threading.Tasks;
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
            CheckAppVersionAndUpdateUI();
        }

        private async void CheckAppVersionAndUpdateUI()
        {
            try
            {
                // �� ���� Ȯ�� ����
                bool isLatestVersion = CheckIfAppIsLatestVersion();

                // ���� �ð�(��: 3��) ��ٸ� ��
                await Task.Delay(3000);

                // ���� �������� ��ȯ �� ����ڿ��� �˸� ����
                if (isLatestVersion)
                {
                    // �ֽ� ������ ��� ����ڿ��� Toast �޽����� �˸�
                    var toast = Toast.Make("���� �ֽ� �����Դϴ�.");
                    await toast.Show();
                }
                else
                {
                    // ������Ʈ�� �ʿ��� ��� ����ڿ��� Toast �޽����� �˸�
                    var toast = Toast.Make("���ο� ������ ���� �ֽ��ϴ�. ������Ʈ�� �ʿ��մϴ�.");
                    await toast.Show();
                }
            }
            catch (Exception ex)
            {
                // ������ �߻��� ��� ����ڿ��� Toast �޽����� ���� �޽����� ����
                var toast = Toast.Make($"�� ������ Ȯ���ϴ� ���� ������ �߻��߽��ϴ�: {ex.Message}");
                await toast.Show();
            }
            finally
            {
                // ��� ��Ȳ���� ���� �������� ��ȯ
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (Application.Current != null)
                    {
                        Application.Current.MainPage = new AppShell();
                    }
                });
            }
        }

        private bool CheckIfAppIsLatestVersion()
        {
            // ���� ��ġ�� ���� ���� ��������
            var currentVersion = VersionTracking.CurrentVersion;

            // �����ͺ��̽����� �ֽ� ���� ���� �������� (������ ����)
            var latestVersion = GetLatestVersionFromDatabase();

            // ���� ������ �ֽ� ���� ��
            return currentVersion == latestVersion;
        }

        // �����ͺ��̽����� �ֽ� ���� ������ ��ȸ�ϴ� ������ �޼���
        private string GetLatestVersionFromDatabase()
        {
            // �����ͺ��̽� ���� ���� ���, ���⼭�� ������ �ֽ� ������ ��ȯ�մϴ�.
            // ���� ���������� �����ͺ��̽� ���� �� ���� ���� �ڵ尡 ���ԵǾ�� �մϴ�.
            return "1.2.2"; // ����: �����ͺ��̽��� ����� �ֽ� ����
        }
    }
}
