using MajorBeat.ViewModels.Musicians;

namespace MajorBeat.Views.Musicians;

public partial class MusicianHomePage : ContentPage
{
	public MusicianHomePage()
	{
		InitializeComponent();
        BindingContext = new HomePageViewModel();
	}

    private async void search_page_btn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new MusicianSearchPage());
    }

    private void home_page_btn_Clicked(object sender, EventArgs e)
    {

    }
}