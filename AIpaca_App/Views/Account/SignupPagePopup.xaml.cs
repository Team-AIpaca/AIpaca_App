using AIpaca_App.Data;
using AIpaca_App.Resources.Localization;
using AIpaca_App.ViewModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using System.Text;
using System.Text.Json;

namespace AIpaca_App.Views.Account;

public partial class SignupPagePopup : Popup
{
    private SettingViewModel _viewModel;

    public SignupPagePopup(SettingViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel; // �ܺο��� ���޹��� MainViewModel �ν��Ͻ��� ���
    }

    private async void OnSignupClicked(object sender, EventArgs e)
    {
        // �Է� �ʵ忡�� ����� �����͸� �����ɴϴ�.
        string email = EmailEntry.Text;
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        // MainViewModel�� SignupAsync �޼��带 ȣ���Ͽ� ȸ�������� �õ��մϴ�.
        bool signupSuccess = await _viewModel.SignupAsync(email, username, password);

        if (signupSuccess)
        {
            // ȸ������ ���� �� ����ڿ��� �˸��� ǥ���ϰ� �˾��� �ݽ��ϴ�.
            await Toast.Make(AppResources.signup_successful, ToastDuration.Long).Show();
            await this.CloseAsync();
        }
        // ���� ��, MainViewModel ���ο��� ���� �޽����� Toast�� ǥ���ϹǷ� ���⼭ ������ ó���� �ʿ� �����ϴ�.
    }
}
