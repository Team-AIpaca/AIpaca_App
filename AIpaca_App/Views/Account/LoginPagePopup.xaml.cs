using AIpaca_App.Data;
using AIpaca_App.Resources.Localization;
using AIpaca_App.ViewModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using System.Text;
using System.Text.Json;

namespace AIpaca_App.Views.Account;

public partial class LoginPagePopup : Popup
{
    private SettingViewModel _viewModel;

    public LoginPagePopup(SettingViewModel viewModel)
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
            await Toast.Make(AppResources.authenticated_success, ToastDuration.Long).Show();
            await this.CloseAsync();
        }
        else
        {
            // �α��� ���� �޽��� (MainViewModel���� ó���� ���� �޽����� ����մϴ�.)
            await Toast.Make(_viewModel.LastErrorMessage, ToastDuration.Long).Show();
        }
    }
}
