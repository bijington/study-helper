namespace StudyHelper.Vision;

public partial class VisionPage : ContentPage
{
    private readonly VisionService _visionService;
    private FileResult? _photo;

    public VisionPage(VisionService visionService)
    {
        InitializeComponent();
        _visionService = visionService;
    }

    private async void OnChooseClicked(object? sender, EventArgs e)
    {
        // Check if capture is supported
        if (!MediaPicker.Default.IsCaptureSupported)
        {
            await DisplayAlertAsync("No camera", "Camera is not available on this device.", "OK");
        }

        try
        {
            _photo = (await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                return await MediaPicker.PickPhotosAsync();
                // return await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                // {
                //     Title = "Take a photo of the room",
                //     MaximumWidth = 1024,
                //     MaximumHeight = 1024,
                //     CompressionQuality = 70
                // });
            }))?.FirstOrDefault();

            if (_photo is not null)
            {
                byte[] imageData;
                await using (var original = await _photo.OpenReadAsync())
                using (var ms = new MemoryStream())
                {
                    await original.CopyToAsync(ms);
                    imageData = ms.ToArray();
                }

                // display captured image
                CapturedImage.IsVisible = true;
                CapturedImage.Source = ImageSource.FromStream(() => new MemoryStream(imageData));
            }
            
            Send.IsEnabled = _photo is not null;
        }
        catch (Exception ex)
        {
            
        }
    }

    private async void OnSendClicked(object? sender, EventArgs e)
    {
        Indicator.IsRunning = true;
        Send.IsEnabled = false;
        Response.Text = await SendImageAndPromptToVisionServiceAsync();
        Indicator.IsRunning = false;
        Send.IsEnabled = true;
    }
    
    private async Task<string> SendImageAndPromptToVisionServiceAsync()
    {
        try
        {
            // Check if vision service is available
            if (!await _visionService.IsAvailableAsync())
            {
                return "Vision service is not available. Please check your configuration.";
            }

            try
            {
                // Analyze the photo
                using var stream = await _photo.OpenReadAsync();
                var result = await _visionService.AnalyzeImageAsync(stream, UserPrompt.Text);

                if (!result.Success)
                {
                    return result.ErrorMessage ?? "Failed to analyze the image.";
                }

                return result.Message ?? "I don't know how to answer that";
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                return $"Failed to capture or process photo: {ex.Message}";
            }
        }
        catch (Exception ex)
        {
            return $"Sorry, I couldn't analyze the room: {ex.Message}";
        }
    }
}