using MajorBeat.ViewModels.Hirers;
using MajorBeat.ViewModels.Users;
using System.Threading.Tasks;

namespace MajorBeat.Views.Hirers;

public partial class HirerHomePage : ContentPage
{

    public HirerHomePage()
    {
        InitializeComponent();
        BindingContext = new HirerHomePageViewModel();

      
    }

    private async void searchBar_Focused(object sender, FocusEventArgs e)
    {
        
    }

    private void searchBar_Unfocused(object sender, FocusEventArgs e)
    {
   
    }

    private async void home_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HirerHomePage());
    }

    private async void search_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HirerSearchPage());//

    }

    private async void profile_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HirerProfilePage());

    }
}
