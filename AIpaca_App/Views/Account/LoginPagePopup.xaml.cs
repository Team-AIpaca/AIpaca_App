using AIpaca_App.Data;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using System.Text;
using System.Text.Json;

namespace AIpaca_App.Views.Account;

public partial class LoginPagePopup : Popup
{
    private string _loginEndpoint;

    public LoginPagePopup()
    {
        InitializeComponent();
        var (baseUrl, loginEndpoint, _) = ApiConfigManager.LoadApiConfig();
        _loginEndpoint = $"{baseUrl}{loginEndpoint}";
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {

        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        var loginData = new
        {
            username,
            password
        };

        var client = new HttpClient();
        var json = JsonSerializer.Serialize(loginData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(_loginEndpoint, content);

            if (response.IsSuccessStatusCode)
            {
                // �α��� ����
                await Toast.Make("User authenticated successfully.", ToastDuration.Long).Show();
                await this.CloseAsync();
            }
            else
            {
                // ���� ������ �н��ϴ�.
                var responseContent = await response.Content.ReadAsStringAsync();
                // JSON ������ �Ľ��մϴ�.
                var responseObject = JsonSerializer.Deserialize<ApiResponse>(responseContent);

                // ���� �޽����� �Բ� ���� �ڵ带 Toast�� ǥ��
                var errorMessage = $"���� {response.StatusCode}: {responseObject?.message ?? "�� �� ���� ������ �߻��߽��ϴ�."}";
                await Toast.Make(errorMessage, ToastDuration.Long).Show();
            }
        }
        catch (Exception ex)
        {
            await Toast.Make($"��Ʈ��ũ ������ �߻��߽��ϴ�: {ex.Message}", ToastDuration.Long).Show();
        }
    }

    private class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? message { get; set; }
        public object? data { get; set; }
    }
}
