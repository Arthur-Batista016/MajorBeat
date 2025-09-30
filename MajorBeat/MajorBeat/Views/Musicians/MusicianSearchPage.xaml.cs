namespace MajorBeat.Views.Musicians;

using System.Threading.Tasks;
using MajorBeat.ViewModels.Users;
using MajorBeat.Views.Users;

public partial class MusicianSearchPage : ContentPage
{
    private SearchBarViewModel Vm => BindingContext as SearchBarViewModel;
    public MusicianSearchPage()
	{
		InitializeComponent();
        BindingContext = new SearchBarViewModel();
	}

    private void searchBar_Focused(object sender, FocusEventArgs e)
    {
        Vm?.onFocus();
    }

    private void searchBar_Unfocused(object sender, FocusEventArgs e)
    {
        Vm.onUnfocus();
    }

    private async void home_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MusicianHomePage());
    }

    private void searchBar_Completed(object sender, EventArgs e)
    {
        var entry = (Entry)sender;
        string textoDigitado = entry.Text;

        Console.WriteLine("Usuário digitou: " + textoDigitado);
    }

    private async void chat_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChatPage());
    }

    private void chat_Clicked_1(object sender, EventArgs e)
    {

    }
}