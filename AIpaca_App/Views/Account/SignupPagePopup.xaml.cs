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
                // HTTP 상태 코드가 201인 경우를 포함하여 200~299 사이일 때 여기서 처리됩니다.
                if (response.StatusCode == System.Net.HttpStatusCode.Created) // 상태 코드 201 확인
                {
                    // 회원가입 성공 처리
                    await Toast.Make("회원가입이 완료되었습니다.", ToastDuration.Long).Show();
                    await this.CloseAsync();
                }
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
            // 네트워크 오류 시 Toast 메시지 표시
            await Toast.Make($"네트워크 오류가 발생했습니다: {ex.Message}", ToastDuration.Long).Show();
        }
    }

    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? message { get; set; }
        public Dictionary<string, string>? data { get; set; }
    }

}
