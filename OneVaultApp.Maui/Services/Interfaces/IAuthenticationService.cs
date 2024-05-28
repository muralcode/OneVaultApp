using IdentityModel.OidcClient;

namespace OneVaultApp.Maui.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResult> LoginAsync();
        Task<string> CallApiAsync(string accessToken);
    }
}
