using MajorBeat.ViewModels.Musicians;
using MajorBeat.Models.Enums;

namespace MajorBeat.Views.Musicians;

public partial class MusicianProfilePage : ContentPage
{
    public MusicianProfilePage()
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