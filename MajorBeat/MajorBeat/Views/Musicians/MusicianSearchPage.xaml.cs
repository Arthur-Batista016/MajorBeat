namespace MajorBeat.Views.Musicians;

using System.Threading.Tasks;
using MajorBeat.Models;
using MajorBeat.ViewModels.Musicians;
using MajorBeat.ViewModels.Users;
using MajorBeat.Views.Users;
using Syncfusion.Maui.Core.Carousel;

public partial class MusicianSearchPage : ContentPage
{
    private readonly MusicianSearchPageViewModel Vm;
    public MusicianSearchPage()
    {
        InitializeComponent();
        BindingContext = new MusicianSearchPageViewModel();
        if (this.BindingContext is MusicianSearchPageViewModel vm)
        {
            Vm = vm; // 2. INICIALIZE A VARIÁVEL AQUI
        }
        else
        {
            // Se o BindingContext não estiver definido, crie um novo
            // (Isso depende de como seu app está estruturado)
            Vm = new MusicianSearchPageViewModel();
            this.BindingContext = Vm;
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is VisualElement element && element.BindingContext is Evento evento)
        {
            // Executa o comando (se quiser manter a lógica dentro da ViewModel)
            Vm.ClickEventCommand.Execute(evento);

            // Navegação async passando o ID
            await Navigation.PushAsync(new EventDetails(evento.idEvento));
        }

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

    private async void chat_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChatPage());
    }

    private async void calendar_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MusicianCalendarPage());
    }
}