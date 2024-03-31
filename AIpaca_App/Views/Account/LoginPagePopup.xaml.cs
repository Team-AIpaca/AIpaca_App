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
        _viewModel = viewModel; // �ܺο��� ���޹��� MainViewModel �ν��Ͻ��� ���
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        bool loginSuccess = await _viewModel.LoginAsync(username, password);

        if (loginSuccess)
        {
            // �α��� ���� �޽���
            await Toast.Make("User authenticated successfully.", ToastDuration.Long).Show();
            await this.CloseAsync();
        }
        else
        {
            // �α��� ���� �޽��� (MainViewModel���� ó���� ���� �޽����� ����մϴ�.)
            await Toast.Make(_viewModel.LastErrorMessage, ToastDuration.Long).Show();
        }
    }
}
