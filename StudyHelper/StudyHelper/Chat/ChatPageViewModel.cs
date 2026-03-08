using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StudyHelper.Chat;

public partial class ChatPageViewModel : ObservableObject
{
    private readonly ChatService _chatService;

    [ObservableProperty]
    private string _userPrompt = string.Empty;
    
    public ObservableCollection<ChatEntry> ChatEntries { get; } = [];

    public ChatPageViewModel(ChatService chatService)
    {
        _chatService = chatService;
    }
    
    [RelayCommand]
    private async Task OnSend()
    {
        var userPrompt = UserPrompt;
        ChatEntries.Add(new ChatEntry { Message = userPrompt, IsUserSent = true });
        UserPrompt = string.Empty;
        
        var responseEntry = new ChatEntry { Message = "...", IsUserSent = false };
        ChatEntries.Add(responseEntry);
        
        var response = await _chatService.SendMessage(userPrompt);

        responseEntry.Message = response.Text;
    }
}