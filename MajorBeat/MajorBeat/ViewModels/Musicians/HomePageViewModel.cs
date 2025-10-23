using CommunityToolkit.Mvvm.ComponentModel;
using MajorBeat.Models;
using MajorBeat.Services.Musicians;
using MajorBeat.Services.Users;
using System.Collections.ObjectModel;

public partial class HomePageViewModel : ObservableObject
{
    EventService _eService;
    private ObservableCollection<Evento> eventos;
    [ObservableProperty]
    private string currentToken;

    public HomePageViewModel()
    {
      
        //MUSICIAN CONSTRUCTOR
        _eService = new EventService();
        currentToken = _eService.Token;
        ActualPosition = 0;
        EventPhoto = new ObservableCollection<string>();
        Eventos = new ObservableCollection<Evento>();

        // Chama o carregamento de eventos de forma assíncrona
        ExibirTodosEventos();
    }

    private ObservableCollection<string> eventPhoto;
    private int actualPosition;

    [ObservableProperty] 
    private bool hasEvent;

    private string namevent;
    private string title;
    private DateTime data;
    public string NameEvent
    {
        get => namevent;
        set
        {
            if (namevent != value)
            {
                namevent = value;
                OnPropertyChanged(nameof(NameEvent));
            }
        }
    }

    public ObservableCollection<Evento> Eventos
    {
        get => eventos;
        set
        {
            eventos = value;
            OnPropertyChanged(nameof(Eventos));
            hasEvent = eventos != null && eventos.Count > 0; // Atualiza HasEvent
        }
    }

    public ObservableCollection<string> EventPhoto
    {
        get => eventPhoto;
        set
        {
            eventPhoto = value;
            OnPropertyChanged(nameof(EventPhoto));
            OnPropertyChanged(nameof(TotalPhotos));
            OnPropertyChanged(nameof(PhotoCounter));
        }
    }

    public int ActualPosition
    {
        get => actualPosition;
        set
        {
            if (actualPosition != value)
            {
                actualPosition = value;
                OnPropertyChanged(nameof(ActualPosition));
                OnPropertyChanged(nameof(PhotoCounter));
            }
        }
    }

    public int TotalPhotos => EventPhoto?.Count ?? 0;

    public string PhotoCounter => $"{ActualPosition + 1}/{TotalPhotos}";

    public async Task EventsIsEmpyty()
    {
        hasEvent = Eventos != null && Eventos.Count > 0;
    }

    public async Task ExibirTodosEventos()
    {
        try
        {
            ObservableCollection<Evento> eventos = await _eService.GetAllEvents();
            Eventos = eventos;
           
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao carregar eventos: {ex.Message}");
        }
    }

    public async Task ChangeEventPhoto()
    {
        EventPhoto = new ObservableCollection<string>
        {
            "panelao.png",
            "birthday.png",
            "bar.png"
        };
    }
}
