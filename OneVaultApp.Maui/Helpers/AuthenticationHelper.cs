using IdentityModel.OidcClient;

namespace OneVaultApp.Maui.Helpers
{
    //Singleton
    public static class AuthenticationHelper
    {
        private static readonly Lazy<OidcClient> _lazyOidcClient = new Lazy<OidcClient>(() =>
        {
            var options = new OidcClientOptions
            {
                Authority = "https://demo.duendesoftware.com",
                ClientId = "interactive.public",
                Scope = "openid profile api",
                RedirectUri = "myapp://callback",
                Browser = new MauiAuthenticationBrowser()
            };
            return new OidcClient(options);
        });

        public static OidcClient OidcClient => _lazyOidcClient.Value;
    }
}
