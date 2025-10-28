using MajorBeat.Models;
using MajorBeat.ViewModels.Hirers;
using MajorBeat.ViewModels.Users;
using System.Threading.Tasks;

namespace MajorBeat.Views.Hirers;

public partial class HirerHomePage : ContentPage
{

    private readonly HirerHomePageViewModel _viewModel;

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

}
