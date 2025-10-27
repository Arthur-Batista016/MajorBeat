using MajorBeat.ViewModels.Users;

namespace MajorBeat.Views.Hirers;

public partial class HirerSearchPage : ContentPage
{
    private SearchBarViewModel Vm => BindingContext as SearchBarViewModel;
    public HirerSearchPage()
	{
		InitializeComponent();
        BindingContext = new SearchBarViewModel();
    }

    private async void SearchButton_Clicked(object sender, EventArgs e)
    {
       

        // Remove o foco do Entry para esconder a caret
        searchBar.Unfocus();
        Vm.UserEntry = "";
    }

    private void searchBar_Focused(object sender, FocusEventArgs e)
    {
        Vm?.onFocus();
        if (Vm.Searchs.Count.Equals(0))
        {
            Vm.RecentSearch = false;
        }
        else
        {
            Vm.RecentSearch = true;
        }
            Vm.BarBackground = "E7E7E7";
    }

    private void searchBar_Unfocused(object sender, FocusEventArgs e)
    {
        Vm.onUnfocus();
    }

    private async void home_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HirerHomePage());
    }

    private void searchBar_Completed(object sender, EventArgs e)
    {
        var entry = (Entry)sender;
        string textoDigitado = entry.Text;

        Console.WriteLine("Usuário digitou: " + textoDigitado);
    }

    private async void chat_Clicked(object sender, EventArgs e)
    {
    
    }

    private void chat_Clicked_1(object sender, EventArgs e)
    {

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Desfoca a barra de busca assim que a página aparece
        Device.BeginInvokeOnMainThread(() =>
        {
            searchBar.Unfocus();
            Vm?.onUnfocus();
        });
    }

    private void MainGrid_Tapped(object sender, EventArgs e)
    {
        // Remove o foco do Entry
        Vm.onUnfocus();
    }
}