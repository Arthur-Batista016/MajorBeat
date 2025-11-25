using System.Threading.Tasks;
using MajorBeat.Models;
using MajorBeat.ViewModels.Users;
using MajorBeat.Views.Users;
using Syncfusion.Maui.Core.Carousel;

namespace MajorBeat.Views.Hirers;

public partial class HirerSearchPage : ContentPage
{
    private readonly SearchBarViewModel Vm;
    public HirerSearchPage()
    {
        InitializeComponent();
        BindingContext = new SearchBarViewModel();

        if (this.BindingContext is SearchBarViewModel vm)
        {
            Vm = vm; // 2. INICIALIZE A VARIÁVEL AQUI
        }
        else
        {
            // Se o BindingContext não estiver definido, crie um novo
            // (Isso depende de como seu app está estruturado)
            Vm = new SearchBarViewModel();
            this.BindingContext = Vm;
        }
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

    private async void search_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HirerSearchPage());

    }

    private async void profile_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HirerProfilePage());

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


    private async void calendar_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateEventPageView());
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is VisualElement element && element.BindingContext is Musico musico)
        {
            // Executa o comando (se quiser manter a lógica dentro da ViewModel)
            Vm.ClickMusicianCommand.Execute(musico);

            // Navegação async passando o ID
            await Navigation.PushAsync(new MusicianDetails(musico.idMusico));
        }

    }
}