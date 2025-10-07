using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MajorBeat.Views.Musicians;

public partial class MusicianProfilePage : ContentPage
{
    public ObservableCollection<string> Images { get; set; } = new();
    public int CurrentIndex { get; set; } = 0;

    public MusicianProfilePage()
	{
		InitializeComponent();
        
        Images.Add("musicianprofile1.png");
        Images.Add("musicianprofile2.png");
        Images.Add("musicianprofile3.png");

        BindingContext = this;

        // Evento para atualizar contador de posição
        ImageCarousel.PositionChanged += OnCarouselPositionChanged;

        // Inicializa label
        UpdatePositionLabel();
    }
    private async void search_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MusicianSearchPage());
    }

    private async void home_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MusicianHomePage());
    }

    private async void profile_page_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MusicianProfilePage());
    }

    private void OnCarouselPositionChanged(object sender, PositionChangedEventArgs e)
    {
        CurrentIndex = e.CurrentPosition;
        UpdatePositionLabel();
    }

    private void UpdatePositionLabel()
    {
        PositionLabel.Text = $"{CurrentIndex + 1}/{Images.Count}";
    }

    private void OnGaleriaTapped(object sender, TappedEventArgs e)
    {
        // Estilos da aba "Galeria"
        TabGaleria.TextColor = Color.FromArgb("#4F1271");
        TabGaleria.FontAttributes = FontAttributes.Bold;
        BoxViewGaleria.HeightRequest = 4;
        BoxViewGaleria.BackgroundColor = Color.FromArgb("#4F1271");

        // Resetando as outras abas
        TabSobre.TextColor = Color.FromArgb("#783F8E");
        TabSobre.FontAttributes = FontAttributes.None;
        BoxViewSobre.HeightRequest = 3;
        BoxViewSobre.BackgroundColor = Color.FromArgb("#783F8E");

        TabAvaliacoes.TextColor = Color.FromArgb("#783F8E");
        TabAvaliacoes.FontAttributes = FontAttributes.None;
        BoxViewAvaliacoes.HeightRequest = 3;
        BoxViewAvaliacoes.BackgroundColor = Color.FromArgb("#783F8E");

        // Mostrar ou ocultar os stacks
        galeriaStack.IsVisible = !galeriaStack.IsVisible;
        sobreStack.IsVisible=false;
        avaliacoesStack.IsVisible=false;
    }

    private void OnSobreTapped(object sender, TappedEventArgs e)
    {
        // Estilos da aba "Sobre"
        TabSobre.TextColor = Color.FromArgb("#4F1271");
        TabSobre.FontAttributes = FontAttributes.Bold;
        BoxViewSobre.HeightRequest = 4;
        BoxViewSobre.BackgroundColor = Color.FromArgb("#4F1271");

        // Resetando as outras abas
        TabGaleria.TextColor = Color.FromArgb("#783F8E");
        TabGaleria.FontAttributes = FontAttributes.None;
        BoxViewGaleria.HeightRequest = 3;
        BoxViewGaleria.BackgroundColor = Color.FromArgb("#783F8E");

        TabAvaliacoes.TextColor = Color.FromArgb("#783F8E");
        TabAvaliacoes.FontAttributes = FontAttributes.None;
        BoxViewAvaliacoes.HeightRequest = 3;
        BoxViewAvaliacoes.BackgroundColor = Color.FromArgb("#783F8E");

        // Mostrar ou ocultar os stacks
        sobreStack.IsVisible = !sobreStack.IsVisible;
        galeriaStack.IsVisible = false;
        avaliacoesStack.IsVisible = false;
    }

    private void OnAvaliacoesTapped(object sender, TappedEventArgs e)
    {
        // Estilos da aba "Avaliações"
        TabAvaliacoes.TextColor = Color.FromArgb("#4F1271");
        TabAvaliacoes.FontAttributes = FontAttributes.Bold;
        BoxViewAvaliacoes.HeightRequest = 4;
        BoxViewAvaliacoes.BackgroundColor = Color.FromArgb("#4F1271");

        // Resetando as outras abas
        TabGaleria.TextColor = Color.FromArgb("#783F8E");
        TabGaleria.FontAttributes = FontAttributes.None;
        BoxViewGaleria.HeightRequest = 3;
        BoxViewGaleria.BackgroundColor = Color.FromArgb("#783F8E");

        TabSobre.TextColor = Color.FromArgb("#783F8E");
        TabSobre.FontAttributes = FontAttributes.None;
        BoxViewSobre.HeightRequest = 3;
        BoxViewSobre.BackgroundColor = Color.FromArgb("#783F8E");

        // Mostrar ou ocultar os stacks
        avaliacoesStack.IsVisible = !avaliacoesStack.IsVisible;
        galeriaStack.IsVisible = false;
        sobreStack.IsVisible = false;
    }
}