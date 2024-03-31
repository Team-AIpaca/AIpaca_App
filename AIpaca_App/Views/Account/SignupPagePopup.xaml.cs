using AIpaca_App.Data;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using System.Text;
using System.Text.Json;

namespace AIpaca_App.Views.Account;

public partial class SignupPagePopup : Popup
{
    private string _signupEndpoint;
    public SignupPagePopup()
    {
        InitializeComponent();
        var (baseUrl, _, signupEndpoint) = ApiConfigManager.LoadApiConfig();
        _signupEndpoint = $"{baseUrl}{signupEndpoint}";
    }
    private async void OnSignupClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        var signupData = new
        {
            email,
            username,
            password
        };

        var client = new HttpClient();
        var json = JsonSerializer.Serialize(signupData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(_signupEndpoint, content);

            if (response.IsSuccessStatusCode)
            {
                // HTTP ���� �ڵ尡 201�� ��츦 �����Ͽ� 200~299 ������ �� ���⼭ ó���˴ϴ�.
                if (response.StatusCode == System.Net.HttpStatusCode.Created) // ���� �ڵ� 201 Ȯ��
                {
                    // ȸ������ ���� ó��
                    await Toast.Make("ȸ�������� �Ϸ�Ǿ����ϴ�.", ToastDuration.Long).Show();
                    await this.CloseAsync();
                }
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
            // ��Ʈ��ũ ���� �� Toast �޽��� ǥ��
            await Toast.Make($"��Ʈ��ũ ������ �߻��߽��ϴ�: {ex.Message}", ToastDuration.Long).Show();
        }
    }

    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? message { get; set; }
        public Dictionary<string, string>? data { get; set; }
    }

}
