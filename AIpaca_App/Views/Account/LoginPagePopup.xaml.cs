using AIpaca_App.Data;
using AIpaca_App.ViewModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using System.Text;
using System.Text.Json;

namespace AIpaca_App.Views.Account;

public partial class LoginPagePopup : Popup
{
    private MainViewModel _viewModel;

    public LoginPagePopup(MainViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel; // 외부에서 전달받은 MainViewModel 인스턴스를 사용
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        bool loginSuccess = await _viewModel.LoginAsync(username, password);

        if (loginSuccess)
        {
            // 로그인 성공 메시지
            await Toast.Make("User authenticated successfully.", ToastDuration.Long).Show();
            await this.CloseAsync();
        }
        else
        {
            // 로그인 실패 메시지 (MainViewModel에서 처리한 에러 메시지를 사용합니다.)
            await Toast.Make(_viewModel.LastErrorMessage, ToastDuration.Long).Show();
        }
    }
}
