using System.Threading.Tasks;

namespace MajorBeat.Views.Musicians;

public partial class MusicianHomePage : ContentPage
{
	public MusicianHomePage()
	{
		InitializeComponent();
	}

    private async void search_page_btn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new MusicianSearchPage());
    }

    private async Task home_page_btn_Clicked(object sender, EventArgs e)
    {
        
    }

    private async void profile_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MusicianProfilePage());
    }
}