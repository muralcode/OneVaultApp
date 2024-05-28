using IdentityModel.Client;
using IdentityModel.OidcClient;
using OneVaultApp.Maui.Helpers;
using OneVaultApp.Maui.Services.Interfaces;
using System.Text.Json;

namespace OneVaultApp.Maui.Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly OidcClient _oidcClient;

        public AuthenticationService() 
        {
            _oidcClient = AuthenticationHelper.OidcClient;
        }
        public async Task<LoginResult> LoginAsync()
        {
            var result = await _oidcClient.LoginAsync(new LoginRequest());
            return result;
        }

        public async Task<string> CallApiAsync(string accessToken)
        {
            using var client = new HttpClient();
            client.SetBearerToken(accessToken);

            var response = await client.GetAsync("https://demo.duendesoftware.com/api/test");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(content).RootElement;
                return JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true });
            }

            return response.ReasonPhrase ?? "API call failed";
        }
    }
}
