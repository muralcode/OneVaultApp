using OneVaultApp.Maui.ViewModels;

namespace OneVaultApp.Maui.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}