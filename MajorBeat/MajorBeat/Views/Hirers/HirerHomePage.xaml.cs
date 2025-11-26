using MajorBeat.Models;
using MajorBeat.ViewModels.Hirers;
using MajorBeat.ViewModels.Users;
using MajorBeat.Views.Users;
using System.Threading.Tasks;

namespace MajorBeat.Views.Hirers;

public partial class HirerHomePage : ContentPage
{

    
private readonly HirerHomePageViewModel _viewModel;
    public HirerHomePage()
    {
        InitializeComponent();

        BindingContext = new HirerHomePageViewModel();
        if (this.BindingContext is HirerHomePageViewModel vm)
        {
            _viewModel = vm; // 2. INICIALIZE A VARIÁVEL AQUI
        }
        else
        {
            // Se o BindingContext não estiver definido, crie um novo
            // (Isso depende de como seu app está estruturado)
            _viewModel = new HirerHomePageViewModel();
            this.BindingContext = _viewModel;
        }

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
        await Navigation.PushAsync(new HirerSearchPage());

    }

    private async void profile_btn_Clicked(object sender, EventArgs e)
    {
       await Navigation.PushAsync(new HirerProfilePage());

    }
    private async void chat_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChatPage());
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is VisualElement element && element.BindingContext is Musico musico)
        {
            // Executa o comando (se quiser manter a lógica dentro da ViewModel)
            _viewModel.ClickMusicianCommand.Execute(musico);

            // Navegação async passando o ID
            await Navigation.PushAsync(new MusicianDetails(musico.idMusico));
        }

    }

    

    private async void calendar_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HirerCalendarPage());//CreateEventPageView()
    }
}
