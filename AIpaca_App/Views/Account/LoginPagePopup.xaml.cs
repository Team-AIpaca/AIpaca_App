using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;

namespace AIpaca_App.Views.Account;

public partial class LoginPagePopup : Popup
{
    public LoginPagePopup()
    {
        InitializeComponent();
    }
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // 로그인 로직 구현
        // 로그인 성공 시 팝업 닫기

        await this.CloseAsync(); // 현재 팝업을 닫는 메서드
    }
}
