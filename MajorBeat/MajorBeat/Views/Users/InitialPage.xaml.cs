using MajorBeat.Views.Hirers;

namespace MajorBeat.Views.Users;

public partial class InitialPage : ContentPage
{
	public InitialPage()
	{
		InitializeComponent();
	}

    private async void register_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserRegisterView());
    }
    

    private  async void login_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateEventPageView());
    }
}