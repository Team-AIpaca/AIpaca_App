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
        // �α��� ���� ����
        // �α��� ���� �� �˾� �ݱ�

        await this.CloseAsync(); // ���� �˾��� �ݴ� �޼���
    }
}
