namespace MajorBeat.Views.Musicians;

public partial class MusicianSearchPage : ContentPage
{
	public MusicianSearchPage()
	{
		InitializeComponent();
	}
    private async void search_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MusicianSearchPage());
    }

    private void home_page_btn_Clicked(object sender, EventArgs e)
    {

    }

    private async void profile_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MusicianProfilePage());
    }
}