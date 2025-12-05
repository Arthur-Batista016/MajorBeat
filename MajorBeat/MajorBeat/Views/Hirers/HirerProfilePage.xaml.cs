using System.Threading.Tasks;
using MajorBeat.ViewModels.Hirers;
using MajorBeat.Views.Musicians;
using MajorBeat.Views.Users;

namespace MajorBeat.Views.Hirers;

public partial class HirerProfilePage : ContentPage
{
    public HirerProfilePage()
    {
        InitializeComponent();
        var viewModel = new ProfileHirerViewModel();
        BindingContext = viewModel;

        // Dispara a chamada assíncrona, não a bloqueia.
        _ = viewModel.LoadHirerDataAsync();
    }

    private async void home_page_btn_Clicked(object sender, EventArgs e)
            => await Navigation.PushAsync(new HirerHomePage());

    private async void search_page_btn_Clicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new HirerSearchPage());

    private async void profile_page_btn_Clicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new HirerProfilePage());

    private async void chat_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChatPage());
    }

    
    private async void calendar_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HirerCalendarPage());
    }
}