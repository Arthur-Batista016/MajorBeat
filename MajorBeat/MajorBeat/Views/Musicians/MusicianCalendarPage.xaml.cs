using MajorBeat.ViewModels.Musician;
using MajorBeat.Views.Users;

namespace MajorBeat.Views.Musicians;

public partial class MusicianCalendarPage : ContentPage
{
	public MusicianCalendarPage()
	{
		InitializeComponent();
        BindingContext = new MusicianCalendarViewModel();
    }

    private async void search_page_btn_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new MusicianSearchPage());

    private async void home_page_btn_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new MusicianHomePage());

    private async void chat_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChatPage());
    }
    private async void calendar_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MusicianCalendarPage());
    }
}