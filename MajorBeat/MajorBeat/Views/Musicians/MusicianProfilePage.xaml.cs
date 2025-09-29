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

        // Exemplo: imagens na pasta Resources/Images
        Images.Add("bandbackground.png");
        Images.Add("casamento.png");
        Images.Add("panelao.png");

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
}