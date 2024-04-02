using AIpaca_App.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using System.Diagnostics;

namespace AIpaca_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // 언어 변경 메시지 수신 등록
            WeakReferenceMessenger.Default.Register<LanguageChangedMessage>(this, (r, m) =>
            {
                // 필요한 UI 갱신 로직
                // 예: 현재 페이지를 새로고침하거나, 전체 Shell 구조를 재구성
                Shell.Current.DisplayAlert("언어 변경", $"언어가 {m.Value}로 변경되었습니다.", "확인");
            });
        }
        
    }
}
