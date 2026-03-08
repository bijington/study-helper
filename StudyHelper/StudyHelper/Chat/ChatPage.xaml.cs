using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudyHelper.Chat;

public partial class ChatPage : ContentPage
{
    private readonly ChatService _chatService;
    private readonly ObservableCollection<ChatEntry> _chatEntries = [];

    public ChatPage(ChatService chatService)
    {
        InitializeComponent();
        
        _chatService = chatService;
        
        ChatHistory.ItemsSource = _chatEntries;
    }

    private async void OnSendClicked(object? sender, EventArgs e)
    {
        var userPrompt = UserPrompt.Text;
        _chatEntries.Add(new ChatEntry { Message = UserPrompt.Text, IsUserSent = true });
        UserPrompt.Text = string.Empty;
        
        var responseEntry = new ChatEntry { Message = "...", IsUserSent = false };
        _chatEntries.Add(responseEntry);
        
        var response = await _chatService.SendMessage(userPrompt);

        responseEntry.Message = response.Text;
    }
}

public class ChatEntry : INotifyPropertyChanged
{
    public required string Message
    {
        get;
        set => SetField(ref field, value);
    }

    public bool IsUserSent { get; init; }
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}