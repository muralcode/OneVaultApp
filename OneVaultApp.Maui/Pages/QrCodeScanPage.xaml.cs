using OneVaultApp.Maui.ViewModels;

namespace OneVaultApp.Maui.Pages;

public partial class QrCodeScanPage : ContentPage
{
    public QrCodeScanPage(QrCodeScanPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();

        // Subscribe to start scan message
        MessagingCenter.Subscribe<QrCodeScanPageViewModel>(this, "StartScan", (sender) =>
        {
             QrCodeReaderControl.StartDetecting();
        });
	}
}