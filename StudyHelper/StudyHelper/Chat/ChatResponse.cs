namespace StudyHelper.Chat;

public class ChatResponse
{
    private ChatResponse(string text, bool isSuccess)
    {
        Text = text;
        IsSuccess = isSuccess;
    }

    public static ChatResponse Ok(string text) => new ChatResponse(text, true);
    
    public static ChatResponse Error(string text) => new ChatResponse(text, false);
    
    public string Text { get; set; }
    public bool IsSuccess { get; set; }
}