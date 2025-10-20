using MajorBeat.ViewModels.Users;

namespace MajorBeat.Views.Users;

public partial class ChatPage : ContentPage
{
	public ChatPage()
	{
		InitializeComponent();
		BindingContext = new ChatViewModel();
	}
}