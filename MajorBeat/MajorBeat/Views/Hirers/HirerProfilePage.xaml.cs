using MajorBeat.ViewModels.Hirers;
using MajorBeat.Views.Musicians;

namespace MajorBeat.Views.Hirers;

public partial class HirerProfilePage : ContentPage
{
	public HirerProfilePage()
	{
		InitializeComponent();
        BindingContext = new ProfileHirerViewModel();
    }

    private async void home_page_btn_Clicked(object sender, EventArgs e)
            => await Navigation.PushAsync(new HirerHomePage());

    private async void search_page_btn_Clicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new HirerSearchPage());

    private async void profile_page_btn_Clicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new HirerProfilePage());
}