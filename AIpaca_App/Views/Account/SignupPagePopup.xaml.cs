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
        _viewModel = viewModel; // 외부에서 전달받은 MainViewModel 인스턴스를 사용
    }

    private async void OnSignupClicked(object sender, EventArgs e)
    {
        // 입력 필드에서 사용자 데이터를 가져옵니다.
        string email = EmailEntry.Text;
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        // MainViewModel의 SignupAsync 메서드를 호출하여 회원가입을 시도합니다.
        bool signupSuccess = await _viewModel.SignupAsync(email, username, password);

        if (signupSuccess)
        {
            // 회원가입 성공 시 사용자에게 알림을 표시하고 팝업을 닫습니다.
            await Toast.Make(AppResources.signup_successful, ToastDuration.Long).Show();
            await this.CloseAsync();
        }
        // 실패 시, MainViewModel 내부에서 오류 메시지를 Toast로 표시하므로 여기서 별도의 처리는 필요 없습니다.
    }
}
