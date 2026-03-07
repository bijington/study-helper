namespace StudyHelper.Models
{
    public class ChatMessage
    {
        public string Text { get; set; }
        public bool IsUser { get; set; }

        public ChatMessage(string text, bool isUser)
        {
            Text = text;
            IsUser = isUser;
        }
    }
}