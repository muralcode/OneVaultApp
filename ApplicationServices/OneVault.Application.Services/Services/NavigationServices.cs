using OneVault.ApplicationCore.Services;


namespace OneVault.ApplicationServices.Services
{
    public sealed class NavigationServices : INavigationService
    {
        public async Task GoToAsync(string route)
        {
            await Shell.Current.GoToAsync(route);
        }

        public async Task GoToAsync(string route, Dictionary<string, object> parameters)
        {
            await Shell.Current.GoToAsync(route, parameters);
        }

        /// <summary>
        /// I added this as an extra incase we would want to navigate to another app.
		/// Used to navigate to different apps. The url is in the format:
		/// appscheme://pageName?parameters&amp;returnUrlreturnUrl.
		/// </summary>
		/// <param name="url">URL</param>
		/// <returns>bool indicating success</returns>
        public async Task<bool> OpenUrlAsync(string url)
        {
            bool success;
            //Have to do this workaround as Launcher.Default.CanOpenAsync and Launcher.Default.TryOpenAsync always return false in Android 11 and above
            //https://github.com/xamarin/Essentials/issues/1958
            try
            {
                success = await Launcher.OpenAsync(url);
            }
            catch (UriFormatException)
            {
                //Expected exception when url is malformed
                throw;
            }
            catch (Exception)
            {
                //Most likey that the url failed because the app is not found on the phone
                success = false;
            }

            return success;
        }

        public async Task PopAsync()
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
