namespace OneVault.ApplicationCore.Services
{
    public interface IPermissionService
    {
        event EventHandler PromptUserToEnableSettings;
        event EventHandler ShowRationaleInfo;
        Task<PermissionStatus> CheckAndRequestCameraPermission();
        Task<PermissionStatus> CheckAndRequestStoragePermission();
    }
}
