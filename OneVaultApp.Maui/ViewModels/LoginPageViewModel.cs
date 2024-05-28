using OneVault.ApplicationCore.Services;
using OneVaultApp.Maui.Services.Interfaces;
using System.Text;
using System.Windows.Input;

namespace OneVaultApp.Maui.ViewModels
{
    public sealed class LoginPageViewModel: BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        private string? _currentAccessToken;
        private string? _editorText;

        public LoginPageViewModel(IAuthenticationService authenticationService, INavigationService navigationService) 
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;

            LoginCommand = new Command(async () => await LoginAsync());
            ApiCommand = new Command(async () => await CallApiAsync());
            NavigateCommand = new Command(async () => await NavigateToScanPage());
        }

        public ICommand LoginCommand { get; }
        public ICommand ApiCommand { get; }
        public ICommand NavigateCommand { get; }

        public string? EditorText
        {
            get => _editorText;
            set => SetProperty(ref _editorText, value);
        }

        public async Task LoginAsync() 
        {
            EditorText = string.Empty;

            var result = await _authenticationService.LoginAsync();

            if (result.IsError)
            {
                EditorText = result.Error;
                return;
            }

            _currentAccessToken = result.AccessToken;

            var sb = new StringBuilder(128);

            sb.AppendLine("claims:");
            foreach (var claim in result.User.Claims)
            {
                sb.AppendLine($"{claim.Type}: {claim.Value}");
            }

            sb.AppendLine();
            sb.AppendLine("access token:");
            sb.AppendLine(result.AccessToken);

            if (!string.IsNullOrWhiteSpace(result.RefreshToken))
            {
                sb.AppendLine();
                sb.AppendLine("refresh token:");
                sb.AppendLine(result.RefreshToken);
            }

            EditorText = sb.ToString();
        }

        private async Task CallApiAsync()
        {
            if (_currentAccessToken is not null)
            {
                var response = await _authenticationService.CallApiAsync(_currentAccessToken);
                EditorText = response;
            }
        }

        private async Task NavigateToScanPage()
        {
            await _navigationService.GoToAsync(AppRoutes.QrCodeScanRoute);
        }
    }
}
