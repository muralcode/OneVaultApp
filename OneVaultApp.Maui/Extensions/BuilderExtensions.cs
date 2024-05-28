using Camera.MAUI;
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OneVault.ApplicationCore.Services;
using OneVault.ApplicationServices.Services;
using OneVaultApp.Maui.Pages;
using OneVaultApp.Maui.Services;
using OneVaultApp.Maui.Services.Interfaces;
using OneVaultApp.Maui.ViewModels;

namespace OneVaultApp.Maui.Extensions
{
    public static class BuilderExtensions
    {
        public static MauiAppBuilder ConfigureMauiInterfaces(this MauiAppBuilder builder)
        {
            builder
                .Services
                    .AddSingleton(Vibration.Default)
                    .AddSingleton(Browser.Default)
                    .AddSingleton(Preferences.Default)
                    .AddSingleton(VersionTracking.Default);
            return builder;
        }

        public static MauiAppBuilder ConfigureOneVaultServices(this MauiAppBuilder builder)
        {
            builder
                .Services
                .AddTransient<INavigationService, NavigationServices>()
                .AddTransient<IPermissionService, PermissionService>();
         
            return builder;
        }

        public static MauiAppBuilder ConfigureOneVaultPages(this MauiAppBuilder builder) 
        {
            builder.Services.TryAddTransient<LoginPage>();
            builder.Services.TryAddTransient<QrCodeScanPage>();

            return builder;
        }

        public static MauiAppBuilder ConfigureOneVaultViewModels(this MauiAppBuilder builder) 
        {
            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<QrCodeScanPageViewModel>();

            return builder;
        }

        public static MauiAppBuilder UseQrCodeReader(this MauiAppBuilder builder)
        {
            builder.UseMauiCameraView();
            return builder;
        }

        public static MauiAppBuilder ConfigureOneVaultRouting(this MauiAppBuilder builder)
        {
            Routing.RegisterRoute(AppRoutes.LoginRoute, typeof(LoginPage));
            Routing.RegisterRoute(AppRoutes.QrCodeScanRoute, typeof(QrCodeScanPage));
            
            return builder;
        }

        public static MauiAppBuilder ConfigureDuendeIdentityApi(this MauiAppBuilder builder) 
        {
            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
            return builder;
        } 
    }
}
