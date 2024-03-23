using System;
using System.Threading.Tasks;
using AIpaca_App.Views;
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
                    // �ֽ� ������ ��� ����ڿ��� �˸�
                    await DisplayAlert("�˸�", "���� �ֽ� �����Դϴ�.", "Ȯ��");
                }
                else
                {
                    // ������Ʈ�� �ʿ��� ��� ����ڿ��� �˸�
                    await DisplayAlert("������Ʈ �ʿ�", "���ο� ������ ���� �ֽ��ϴ�. \n������Ʈ�� �ʿ��մϴ�.", "Ȯ��");
                }
            }
            catch (Exception ex)
            {
                // ������ �߻��� ��� ����ڿ��� ���� �ڼ��� ���� �޽����� ����
                await DisplayAlert("����", $"�� ������ Ȯ���ϴ� ���� ������ �߻��߽��ϴ�: {ex.Message}", "Ȯ��");
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
            return "1.0.0"; // ����: �����ͺ��̽��� ����� �ֽ� ����
        }
    }
}
