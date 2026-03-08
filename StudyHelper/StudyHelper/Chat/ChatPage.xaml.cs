using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudyHelper.Chat;

public partial class ChatPage : ContentPage
{
    public ChatPage(ChatPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}