using OneVault.ApplicationCore.Services;

namespace OneVault.ApplicationServices.Services
{
    public sealed class PermissionService : IPermissionService
    {
        public event EventHandler PromptUserToEnableSettings;
        public event EventHandler ShowRationaleInfo;

        public Task<PermissionStatus> CheckAndRequestCameraPermission()
        {
            return CheckAndRequestPermission<Permissions.Camera>();
        }

        public Task<PermissionStatus> CheckAndRequestStoragePermission()
        {
#if IOS
			return CheckAndRequestPermission<Permissions.Photos>();
#elif ANDROID
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Tiramisu)
            {
                return CheckAndRequestPermission<Platforms.Android.Permissions.ReadMediaPermission>();
            }
            else
            {
                return CheckAndRequestPermission<Permissions.StorageWrite>();
            }
#else
			return Task.FromResult(PermissionStatus.Unknown);
#endif
        }

        private async Task<PermissionStatus> CheckAndRequestPermission<TPermission>()
            where TPermission : Permissions.BasePermission, new()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<TPermission>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                PromptUserToEnableSettings?.Invoke(this, new EventArgs());
                return status;
            }

            if (Permissions.ShouldShowRationale<TPermission>())
            {
                // Prompt the user with additional information as to why the permission is needed
                ShowRationaleInfo?.Invoke(this, new EventArgs());
            }

            status = await Permissions.RequestAsync<TPermission>();

            return status;
        }
    }
}
