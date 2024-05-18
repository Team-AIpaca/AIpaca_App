using System.Diagnostics;
using System.Reflection;
using System.Security;
using System.Xml.Linq;

namespace AIpaca_App.Data
{
    public static class ApiConfigManager
    {
        public static (string BaseUrl, string LoginEndpoint, string SignupEndpoint, string gptEndpoint, string GeminiEndpoint, 
            string PingEndpoint, string googletrans, string papago, string deepl, string libre) LoadApiConfig()
        {
            var assembly = typeof(ApiConfigManager).GetTypeInfo().Assembly;
            var resourceName = "AIpaca_App.Resources.Config.ApiConfig.xml";

            using var stream = assembly.GetManifestResourceStream(resourceName) ?? throw new InvalidOperationException("ApiConfig.xml 리소스를 찾을 수 없습니다.");
            using var reader = new StreamReader(stream);
            var doc = XDocument.Load(reader);
            var apiSettings = doc.Descendants("ApiSettings").First();

            var baseUrl = apiSettings.Element("BaseUrl")?.Value ?? string.Empty;
            var loginEndpoint = apiSettings.Element("LoginEndpoint")?.Value ?? string.Empty;
            var signupEndpoint = apiSettings.Element("SignupEndpoint")?.Value ?? string.Empty;
            var gptEndpoint = apiSettings.Element("GPTEndpoint")?.Value ?? string.Empty;
            var geminiEndpoint = apiSettings.Element("GeminiEndpoint")?.Value ?? string.Empty;
            var pingEndpoint = apiSettings.Element("PingEndpoint")?.Value ?? string.Empty;
            var googletrans = apiSettings.Element("GoogleTransEndpoint")?.Value ?? string.Empty;
            var papago = apiSettings.Element("PapagoTransEndpoint")?.Value ?? string.Empty;
            var deepl = apiSettings.Element("DeepLTransEndpoint")?.Value ?? string.Empty;
            var libre = apiSettings.Element("LibretranslateTransEndpoint")?.Value ?? string.Empty;

            return (baseUrl, loginEndpoint, signupEndpoint, gptEndpoint, geminiEndpoint, pingEndpoint, googletrans, papago, deepl, libre);
        }
    }
}
