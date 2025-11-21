using MajorBeat.ViewModels.Musicians;

namespace MajorBeat.Views.Musicians;

public partial class EventDetails : ContentPage
{
	public EventDetails(long id)
	{
		InitializeComponent();
		BindingContext = new EventDetailsViewModel(id);
    }

    private async void search_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MusicianSearchPage());
    }

    private async void home_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MusicianHomePage());
    }

    private async void profile_page_btn_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new MusicianProfilePage());
    }
    private async void voltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}