namespace StudyHelper.Chat;

public partial class ChatPage : ContentPage
{
    private readonly ChatService _chatService;

    public ChatPage(ChatService chatService)
    {
        InitializeComponent();
        
        _chatService = chatService;
    }

    private async void OnSendClicked(object? sender, EventArgs e)
    {
        var response = await _chatService.SendMessage(UserPrompt.Text);

        if (response.IsSuccess)
        {
            Response.Text = response.Text;
        }
    }
}