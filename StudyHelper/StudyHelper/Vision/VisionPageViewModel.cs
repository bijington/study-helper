using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StudyHelper.Vision;

public partial class VisionPageViewModel : ObservableObject
{
    private readonly VisionService _visionService;
    private FileResult? _photo;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    private ImageSource? _capturedImageSource;
    
    [ObservableProperty]
    private string _response = string.Empty;
    
    [ObservableProperty]
    private string _userPrompt = string.Empty;
    
    [ObservableProperty]
    private bool _isProcessingRequest;
    
    public VisionPageViewModel(VisionService visionService)
    {
        _visionService = visionService;
    }
    
    [RelayCommand]
    private async Task OnCapture()
    {
        // Check if capture is supported
        if (!MediaPicker.Default.IsCaptureSupported)
        {
            //await DisplayAlertAsync("No camera", "Camera is not available on this device.", "OK");
        }

        try
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Take a photo of the room",
                MaximumWidth = 1024,
                MaximumHeight = 1024,
                CompressionQuality = 70
            });

            await LoadImageAsync(photo);
        }
        catch (Exception ex)
        {
            //await DisplayAlertAsync("Capture issue", ex.Message, "OK");
        }
    }
    
    [RelayCommand]
    private async Task OnChoose()
    {
        try
        {
            var photo = (await MediaPicker.PickPhotosAsync()).FirstOrDefault();
            
            await LoadImageAsync(photo);
        }
        catch (Exception ex)
        {
            //await DisplayAlertAsync("Choose issue", ex.Message, "OK");
        }
    }

    private async Task LoadImageAsync(FileResult? photo)
    {
        try
        {
            _photo = photo;

            if (_photo is not null)
            {
                byte[] imageData;
                await using (var original = await _photo.OpenReadAsync())
                using (var ms = new MemoryStream())
                {
                    await original.CopyToAsync(ms);
                    imageData = ms.ToArray();
                }

                CapturedImageSource = ImageSource.FromStream(() => new MemoryStream(imageData));
            }
        }
        catch (Exception ex)
        {
            //await DisplayAlertAsync("Image load issue", ex.Message, "OK");
        }
    }

    [RelayCommand(CanExecute = nameof(CanSend))]
    private async Task OnSend()
    {
        IsProcessingRequest = true;
        
        Response = await SendImageAndPromptToVisionServiceAsync();
        
        IsProcessingRequest = false;
    }
    
    private bool CanSend() => CapturedImageSource is not null;
    
    private async Task<string> SendImageAndPromptToVisionServiceAsync()
    {
        try
        {
            // Check if vision service is available
            if (!await _visionService.IsAvailableAsync())
            {
                return "Vision service is not available. Please check your configuration.";
            }

            if (_photo is null)
            {
                return "No image available. Please check your configuration.";
            }

            try
            {
                // Analyze the photo
                await using var stream = await _photo.OpenReadAsync();
                var result = await _visionService.AnalyzeImageAsync(stream, UserPrompt);

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
            return $"Sorry, I couldn't analyze the request: {ex.Message}";
        }
    }
}