namespace StudyHelper.Vision;

public partial class VisionPage : ContentPage
{
    public VisionPage(VisionPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}