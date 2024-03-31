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
                // 로그인 성공
                await Toast.Make("User authenticated successfully.", ToastDuration.Long).Show();
                await this.CloseAsync();
            }
            else
            {
                // 응답 본문을 읽습니다.
                var responseContent = await response.Content.ReadAsStringAsync();
                // JSON 응답을 파싱합니다.
                var responseObject = JsonSerializer.Deserialize<ApiResponse>(responseContent);

                // 오류 메시지와 함께 상태 코드를 Toast로 표시
                var errorMessage = $"오류 {response.StatusCode}: {responseObject?.message ?? "알 수 없는 오류가 발생했습니다."}";
                await Toast.Make(errorMessage, ToastDuration.Long).Show();
            }
        }
        catch (Exception ex)
        {
            await Toast.Make($"네트워크 오류가 발생했습니다: {ex.Message}", ToastDuration.Long).Show();
        }
    }

    private class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? message { get; set; }
        public object? data { get; set; }
    }
}
