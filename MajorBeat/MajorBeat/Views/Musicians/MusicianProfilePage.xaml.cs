using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MajorBeat.ViewModels.Musicians;
using System;
using MajorBeat.Models.Enums;

namespace MajorBeat.Views.Musicians;

public partial class MusicianProfilePage : ContentPage
{
    private ProfileMusicianViewModel _viewModel;

    public MusicianProfilePage()
    {
        InitializeComponent();

        _viewModel = new ProfileMusicianViewModel
        {
            // Atribui valores diretamente ao ViewModel
            Username = "Marquinhos",
            Nome = "Marcos José",
            Biografia = "Oi, eu sou o Marquinhos. Minha música é um pedaço de mim, uma mistura das minhas raízes e das minhas descobertas pelo caminho. Minha música é essa mistura de influências, uma mistura que vem do coração, mas também de uma busca constante por novas formas de me expressar.",

            // Atribuindo uma lista de instrumentos e gêneros
            Instrumentos = new List<NomeInstrumento> { NomeInstrumento.GUITARRA },
            Generos = new List<NomeGenero> { NomeGenero.SERTANEJO },

            // Outros campos
            Email = "marcos.jose123@gmail.com",
            Logradouro = "R. Alcântara, 113 - Vila Guilherme,\nSão Paulo - SP, 02110-010",
            Telefone = "(11) 99999-9999",
            LinkFacebook = "marcos.jose",
            LinkLinkdin = "marcos.jose",
            LinkInsta = "marcos.jose",
            LinkTwitter = "marcos.jose"
        };

        // Define o BindingContext
        BindingContext = _viewModel;

        // Configuração do evento de mudança de posição no Carousel
        ImageCarousel.PositionChanged += OnCarouselPositionChanged;
    }

    // Evento para atualizar a posição
    private void OnCarouselPositionChanged(object sender, PositionChangedEventArgs e)
    {
        // Atualiza o índice da imagem no ViewModel
        _viewModel.CurrentIndex = e.CurrentPosition;

        // Atualiza o texto da posição (após o binding no XAML)
        UpdatePositionLabel();
    }

    // Atualiza o texto da label de posição da imagem
    private void UpdatePositionLabel()
    {
        PositionLabel.Text = $"{_viewModel.CurrentIndex + 1}/{_viewModel.Images.Count}";
    }

    // Métodos de navegação entre páginas
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

    // Estilos e visibilidade das abas - Galeria
    private void OnGaleriaTapped(object sender, TappedEventArgs e)
    {
        UpdateTabStyles("Galeria");

        // Mostrar ou ocultar os stacks
        galeriaStack.IsVisible = !galeriaStack.IsVisible;
        sobreStack.IsVisible = false;
        avaliacoesStack.IsVisible = false;
    }

    // Estilos e visibilidade das abas - Sobre
    private void OnSobreTapped(object sender, TappedEventArgs e)
    {
        UpdateTabStyles("Sobre");

        // Mostrar ou ocultar os stacks
        sobreStack.IsVisible = !sobreStack.IsVisible;
        galeriaStack.IsVisible = false;
        avaliacoesStack.IsVisible = false;
    }

    // Estilos e visibilidade das abas - Avaliações
    private void OnAvaliacoesTapped(object sender, TappedEventArgs e)
    {
        UpdateTabStyles("Avaliacoes");

        // Mostrar ou ocultar os stacks
        avaliacoesStack.IsVisible = !avaliacoesStack.IsVisible;
        galeriaStack.IsVisible = false;
        sobreStack.IsVisible = false;
    }

    // Função auxiliar para atualizar as cores e estilos das abas
    private void UpdateTabStyles(string tab)
    {
        switch (tab)
        {
            case "Galeria":
                TabGaleria.TextColor = Color.FromArgb("#4F1271");
                TabGaleria.FontAttributes = FontAttributes.Bold;
                BoxViewGaleria.HeightRequest = 4;
                BoxViewGaleria.BackgroundColor = Color.FromArgb("#4F1271");

                ResetOtherTabs("Galeria");
                break;

            case "Sobre":
                TabSobre.TextColor = Color.FromArgb("#4F1271");
                TabSobre.FontAttributes = FontAttributes.Bold;
                BoxViewSobre.HeightRequest = 4;
                BoxViewSobre.BackgroundColor = Color.FromArgb("#4F1271");

                ResetOtherTabs("Sobre");
                break;

            case "Avaliacoes":
                TabAvaliacoes.TextColor = Color.FromArgb("#4F1271");
                TabAvaliacoes.FontAttributes = FontAttributes.Bold;
                BoxViewAvaliacoes.HeightRequest = 4;
                BoxViewAvaliacoes.BackgroundColor = Color.FromArgb("#4F1271");

                ResetOtherTabs("Avaliacoes");
                break;
        }
    }

    // Resetando as outras abas
    private void ResetOtherTabs(string activeTab)
    {
        if (activeTab != "Galeria")
        {
            TabGaleria.TextColor = Color.FromArgb("#783F8E");
            TabGaleria.FontAttributes = FontAttributes.None;
            BoxViewGaleria.HeightRequest = 3;
            BoxViewGaleria.BackgroundColor = Color.FromArgb("#783F8E");
        }

        if (activeTab != "Sobre")
        {
            TabSobre.TextColor = Color.FromArgb("#783F8E");
            TabSobre.FontAttributes = FontAttributes.None;
            BoxViewSobre.HeightRequest = 3;
            BoxViewSobre.BackgroundColor = Color.FromArgb("#783F8E");
        }

        if (activeTab != "Avaliacoes")
        {
            TabAvaliacoes.TextColor = Color.FromArgb("#783F8E");
            TabAvaliacoes.FontAttributes = FontAttributes.None;
            BoxViewAvaliacoes.HeightRequest = 3;
            BoxViewAvaliacoes.BackgroundColor = Color.FromArgb("#783F8E");
        }
    }
}