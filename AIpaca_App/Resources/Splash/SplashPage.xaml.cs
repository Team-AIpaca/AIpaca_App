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
                // 앱 버전 확인 로직
                bool isLatestVersion = CheckIfAppIsLatestVersion();

                // 일정 시간(예: 3초) 기다린 후
                await Task.Delay(3000);

                // 메인 페이지로 전환 전 사용자에게 알림 제공
                if (isLatestVersion)
                {
                    // 최신 버전인 경우 사용자에게 알림
                    await DisplayAlert("알림", "앱이 최신 버전입니다.", "확인");
                }
                else
                {
                    // 업데이트가 필요한 경우 사용자에게 알림
                    await DisplayAlert("업데이트 필요", "새로운 버전의 앱이 있습니다. \n업데이트가 필요합니다.", "확인");
                }
            }
            catch (Exception ex)
            {
                // 오류가 발생한 경우 사용자에게 보다 자세한 오류 메시지를 제공
                await DisplayAlert("오류", $"앱 버전을 확인하는 도중 오류가 발생했습니다: {ex.Message}", "확인");
            }
            finally
            {
                // 모든 상황에서 메인 페이지로 전환
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
            // 현재 설치된 앱의 버전 가져오기
            var currentVersion = VersionTracking.CurrentVersion;

            // 데이터베이스에서 최신 버전 정보 가져오기 (가상의 예시)
            var latestVersion = GetLatestVersionFromDatabase();

            // 현재 버전과 최신 버전 비교
            return currentVersion == latestVersion;
        }

        // 데이터베이스에서 최신 버전 정보를 조회하는 가상의 메서드
        private string GetLatestVersionFromDatabase()
        {
            // 데이터베이스 로직 구현 대신, 여기서는 가상의 최신 버전을 반환합니다.
            // 실제 구현에서는 데이터베이스 접속 및 쿼리 실행 코드가 포함되어야 합니다.
            return "1.0.0"; // 가정: 데이터베이스에 저장된 최신 버전
        }
    }
}
