using MajorBeat.Views.Musicians;

namespace MajorBeat.Views.Hirers;

public partial class HirerProfilePage : ContentPage
{
	public HirerProfilePage()
	{
		InitializeComponent();
	}

    private async void home_page_btn_Clicked(object sender, EventArgs e)
            => await Navigation.PushAsync(new MusicianHomePage());

    private async void search_page_btn_Clicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new MusicianSearchPage());

    private async void profile_page_btn_Clicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new MusicianProfilePage());
}