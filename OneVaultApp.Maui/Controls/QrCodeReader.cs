using System.Windows.Input;
using Camera.MAUI;

namespace OneVaultApp.Maui.Controls;

public class QrCodeReader : CameraView, IDisposable
{
    public static readonly BindableProperty OnBarcodeDetectedCommandProperty = BindableProperty.Create(nameof(OnBarcodeDetectedCommand), typeof(ICommand), typeof(QrCodeReader));
    public static readonly BindableProperty BarcodeTextProperty = BindableProperty.Create(nameof(BarcodeText), typeof(string), typeof(QrCodeReader));

    private CameraInfo _cameraInfo;

    public QrCodeReader()
    {
        BarCodeDetectionEnabled = false;
        AutoStartPreview = false;
        IsEnabled = false;
        IsVisible = false;
        HeightRequest = 300;
        WidthRequest = 300;
        CamerasLoaded += OnCamerasLoaded;
        BarcodeDetected += OnBarcodeDetected;
    }

    public ICommand OnBarcodeDetectedCommand
    {
        get => (ICommand)GetValue(OnBarcodeDetectedCommandProperty);
        set => SetValue(OnBarcodeDetectedCommandProperty, value);
    }

    public string BarcodeText
    {
        get => (string)GetValue(BarcodeTextProperty);
        set => SetValue(BarcodeTextProperty, value);
    }

    public void Dispose()
    {
        BarcodeDetected -= OnBarcodeDetected;
        CamerasLoaded -= OnCamerasLoaded;
    }

    public void StartDetecting()
    {
        IsEnabled = true;
        IsVisible = true;
        AutoStartPreview = true;
        Camera = _cameraInfo;
        BarCodeDetectionEnabled = true;

        Dispatcher.DispatchAsync(async () =>
        {
            await StopCameraAsync();
            await StartCameraAsync();
        });
    }

    public void StopDetecting()
    {
        IsEnabled = false;
        IsVisible = false;
        AutoStartPreview = false;
        BarCodeDetectionEnabled = false;

        Dispatcher.DispatchAsync(async () =>
        {
            await StopCameraAsync();
        });
    }

    private void OnCamerasLoaded(object sender, EventArgs e)
    {
        if (Cameras.Any())
            _cameraInfo = Cameras.FirstOrDefault(c => c.Position == CameraPosition.Back) ?? Cameras.First();
    }

    private void OnBarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            string barcodeValue = string.Empty;

            if (e.Result.Any())
                barcodeValue = e.Result.First().Text;

            BarcodeText = barcodeValue;
            OnBarcodeDetectedCommand.Execute(barcodeValue);

            StopDetecting();
        });
    }
}