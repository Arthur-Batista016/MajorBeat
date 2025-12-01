

using MajorBeat.Models;
using MajorBeat.Views.Hirers;
using MajorBeat.Views.Users;
using Syncfusion.Maui.Core.Carousel;

namespace MajorBeat.Views.Musicians;

public partial class MusicianHomePage : ContentPage
{
    private readonly HomePageViewModel _viewModel;
    public MusicianHomePage()
	{
		InitializeComponent();
       
        BindingContext = new HomePageViewModel();
        if (this.BindingContext is HomePageViewModel vm)
        {
            _viewModel = vm; // 2. INICIALIZE A VARIÁVEL AQUI
        }
        else
        {
            // Se o BindingContext não estiver definido, crie um novo
            // (Isso depende de como seu app está estruturado)
            _viewModel = new HomePageViewModel();
            this.BindingContext = _viewModel;
        }
    }


    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is VisualElement element && element.BindingContext is Evento evento)
        {
            // Executa o comando (se quiser manter a lógica dentro da ViewModel)
            _viewModel.ClickEventCommand.Execute(evento);

            // Navegação async passando o ID
            await Navigation.PushAsync(new EventDetails(evento.idEvento));
        }

    }

    private async void search_page_btn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new MusicianSearchPage());
    }

    private void home_page_btn_Clicked(object sender, EventArgs e)
    {
        
    }
    private async void chat_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChatPage());
    }

    private void searchBar_Completed(object sender, EventArgs e)
    {

    }

    private void searchBar_Focused(object sender, FocusEventArgs e)
    {

    }

    private void searchBar_Unfocused(object sender, FocusEventArgs e)
    {

    }
    private async void notification_page(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotificationPage());
    }
}