using MajorBeat.ViewModels.Users;
using MajorBeat.Views.Musicians;

namespace MajorBeat.Views.Users;

public partial class ChatPage : ContentPage
{
	public ChatPage()
	{
		InitializeComponent();
		BindingContext = new ChatViewModel();
	}


    private async void profile_btn_Clicked(object sender, EventArgs e)
    {
       // await Navigation.PushAsync(new HirerProfilePage());

    }
}