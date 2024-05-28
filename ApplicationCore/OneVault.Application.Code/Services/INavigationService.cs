namespace OneVault.ApplicationCore.Services
{
    public interface INavigationService
    {
        Task GoToAsync(string route);
        Task GoToAsync(string route, Dictionary<string, object> parameters);
        Task<bool> OpenUrlAsync(string url);
    }
}
