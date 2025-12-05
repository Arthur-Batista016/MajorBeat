using System.Threading.Tasks;
using MajorBeat.ViewModels.Users;
using MajorBeat.Views.Musicians;

namespace MajorBeat.Views.Hirers;

public partial class MusicianDetails : ContentPage
{
	public MusicianDetails(long id_musico)
	{
		InitializeComponent();
		BindingContext = new DetailsViewModel(id_musico);
	}

   
    private async void calendar_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HirerCalendarPage());
    }
    private async void search_page_btn_Clicked(object sender, EventArgs e) =>
       await Navigation.PushAsync(new HirerSearchPage());

    private async void home_page_btn_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new HirerHomePage());

}