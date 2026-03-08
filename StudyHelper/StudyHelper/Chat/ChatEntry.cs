using CommunityToolkit.Mvvm.ComponentModel;

namespace StudyHelper.Chat;

public partial class ChatEntry : ObservableObject
{
    [ObservableProperty]
    private string _message = string.Empty;

    public bool IsUserSent { get; init; }
}