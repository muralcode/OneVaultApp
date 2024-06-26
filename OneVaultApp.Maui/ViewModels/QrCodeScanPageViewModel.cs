﻿using OneVault.ApplicationCore.Services;
using System.Windows.Input;

namespace OneVaultApp.Maui.ViewModels
{
    public sealed class QrCodeScanPageViewModel: BaseViewModel
    {
       private readonly IPermissionService _permissionService;
       private readonly INavigationService _navigationService;

       private string? _qrCodeText;
       private bool _isPermissionGranted = false;

        public QrCodeScanPageViewModel(IPermissionService permissionService, INavigationService navigationService) 
        {
            _permissionService = permissionService;
            _navigationService = navigationService;

            StartScanCommand = new Command(StartScan);
            QrCodeDetectedCommand = new Command<string>(QrCodeDetected);
            BackCommand = new Command(NavigateBack);
        }

        public ICommand StartScanCommand { get; }
        public ICommand QrCodeDetectedCommand { get; }
        public ICommand BackCommand { get; }

        public string? QrCodeText
        {
            get => _qrCodeText;
            set => SetProperty(ref _qrCodeText, value);
        }
        
        private async void StartScan()
        {
            try
            {
                if (!_isPermissionGranted)
                {
                    var permission = await _permissionService.CheckAndRequestCameraPermission();

                    if (permission != PermissionStatus.Granted)
                    {
                        await Shell.Current.DisplayAlert("Error", "Permission Denied", "Ok");
                        return;
                    }

                    _isPermissionGranted = true;
                }

                MessagingCenter.Send(this, "StartScan");
            }
            catch (Exception ex)
            {
                // Log the exception
                await Shell.Current.DisplayAlert("Error", "An error occurred while requesting permissions", "Cancel");
            }
        }

        private void QrCodeDetected(string barcodeText)
        {
            if (string.IsNullOrEmpty(barcodeText))
                return;

            QrCodeText = barcodeText;
        }

        private async void NavigateBack() 
        {
           await _navigationService.PopAsync();   
        } 
    }
}
