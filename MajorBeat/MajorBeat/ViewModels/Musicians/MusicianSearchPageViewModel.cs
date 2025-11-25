using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Enums;
using MajorBeat.Models;
using MajorBeat.Services.Users;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Musicians
{
    public partial class MusicianSearchPageViewModel : ObservableObject
    {




        EventService _eService;



        [ObservableProperty]
        ObservableCollection<Evento> eventos;

        [ObservableProperty]
        ObservableCollection<Musico> musicos;





        //METODOS DE PESQUISA
        [ObservableProperty]
        public bool barVisibility = false;

        [ObservableProperty]
        public RoundRectangle barFormat = new RoundRectangle { CornerRadius = new CornerRadius(10, 10, 10, 10) };

        [ObservableProperty]
        public string barBackground = "#AE92BD";

        [ObservableProperty]
        public Color placeholderColor = Color.FromArgb("#FFFFFF");

        public int RecentSearchHeight => 50 + (Searchs?.Count ?? 0) * 50;

        [ObservableProperty]
        public Color textColor = Color.FromArgb("#FFFFFF");

        [ObservableProperty]
        public string lupa = "lupainverted.png";


        private string userEntry;

        public string UserEntry
        {
            get => userEntry;
            set
            {
                if (userEntry != value)
                {
                    userEntry = value;
                    OnPropertyChanged(nameof(UserEntry));
                    searchByLetters();
                }
            }
        }

        private ObservableCollection<string> filteredSearchs = new ObservableCollection<string>();
        public ObservableCollection<string> FilteredSearchs
        {
            get => filteredSearchs;
            set
            {
                filteredSearchs = value;
                OnPropertyChanged(nameof(FilteredSearchs));
            }
        }



        [ObservableProperty]
        public bool recentSearch = false;

        [ObservableProperty]
        public ObservableCollection<String> searchs;

        [ObservableProperty]
        public ObservableCollection<NomeGenero> generos = new ObservableCollection<NomeGenero>
    {
        NomeGenero.AXÉ,
        NomeGenero.CLÁSSICO,
        NomeGenero.ELETRONICO,
        NomeGenero.FUNK,
        NomeGenero.RAP,           // Hip-Hop/Rap
        NomeGenero.JAZZ,
        NomeGenero.POP,
        NomeGenero.SAMBA,
        NomeGenero.BLUES,
        NomeGenero.FORRÓ,
        NomeGenero.GOSPEL,
        NomeGenero.TRAP,          // Infantil (você pode criar um enum separado se quiser)
        NomeGenero.METAL,
        NomeGenero.ROCK,
        NomeGenero.SERTANEJO
        };





        public ICommand SearchCommand { get; set; }
        public ICommand ClickEventCommand { get; set; }

        public void InicializarCommands()
        {
            Searchs = new ObservableCollection<string>();
            SearchCommand = new Command(async () => await search());
            ClickEventCommand = new Command<Evento>(async (evento) => await EventTapped(evento));

        }
        private async Task EventTapped(Evento evento)
        {

            if (evento == null)
                return;

            long id = evento.idEvento;




        }

        public async Task onFocus()
        {
            if (Searchs.Count() == 0)
            {
                BarFormat = new RoundRectangle { CornerRadius = new CornerRadius(10, 10, 10, 10) };
            }
            else if (Searchs.Count() > 0)
            {

                BarFormat = new RoundRectangle { CornerRadius = new CornerRadius(10, 10, 0, 0) };
                RecentSearch = true;
            }

            BarBackground = "#E7E7E7";
            BarVisibility = true;
            PlaceholderColor = Color.FromArgb("#4F1271");
            TextColor = Color.FromArgb("#4F1271");
            Lupa = "lupafocus.png";


        }

        public async Task onUnfocus()
        {
            BarBackground = "#AE92BD";
            BarFormat = new RoundRectangle { CornerRadius = new CornerRadius(10, 10, 10, 10) };
            BarVisibility = false;
            PlaceholderColor = Color.FromArgb("#FFFFFF");
            TextColor = Color.FromArgb("#FFFFFF");
            Lupa = "lupainverted.png";
            RecentSearch = false;

        }

        public async Task recentSearchs()
        {
            if (Searchs.Count() != 0)
            {
                RecentSearch = true;
            }

        }

        public async Task search()
        {

            Searchs.Add(userEntry);
            BarBackground = "#4F1271";
            OnPropertyChanged(nameof(RecentSearchHeight));
            await onUnfocus();

        }

        public async Task searchByLetters()
        {

            if (string.IsNullOrWhiteSpace(UserEntry))
            {
                FilteredSearchs = new ObservableCollection<string>(Searchs);
            }
            else
            {

                var filtrados = Searchs
                    .Where(i => i.ToString().ToLowerInvariant().Contains(UserEntry, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                FilteredSearchs = new ObservableCollection<string>(filtrados);


            }


        }















        ///MUSICIAN SEARCH PAGE 




        public MusicianSearchPageViewModel()
        {

            InicializarCommands();
            onUnfocus();
            _eService = new EventService();


            TipoEventoCommand = new AsyncRelayCommand<TipoEvento>(BuscarEventosTipo);
            BackEventCommand = new Command(async () => await BackEventChoosed());

        }


        [ObservableProperty]
        public bool musicianNoSelect = true;

        [ObservableProperty]
        public bool isSearchEvent = false;

        [ObservableProperty]
        public bool findEvents = true;

        [ObservableProperty]
        public bool noEvents = false;



        public ICommand TipoEventoCommand { get; set; }
        public ICommand BackEventCommand { get; set; }


        public async Task BackEventChoosed()
        {
            MusicianNoSelect = true;
            IsSearchEvent = false;
            FindEvents = false;
            NoEvents = false;
            Eventos = new ObservableCollection<Evento>();
        }



        private async Task<ObservableCollection<Evento>> BuscarEventosTipo(TipoEvento evento)
        {
            try
            {
                var resultado = await _eService.GetEventByTipoEvento(evento);
                Eventos = resultado;

                if (Eventos?.Count > 0)
                {

                    MusicianNoSelect = false;
                    IsSearchEvent = true;
                    FindEvents = true;
                    NoEvents = false;


                }
                else
                {
                    MusicianNoSelect = false;
                    NoEvents = true;
                    IsSearchEvent = false;
                    FindEvents = false;
                    return Eventos = new ObservableCollection<Evento>();
                }

                return Eventos;
            }
            catch (Exception ex)
            {
                MusicianNoSelect = false;
                NoEvents = true;
                IsSearchEvent = false;
                FindEvents = false;
                return Eventos = new ObservableCollection<Evento>();
            }
        }


    }
}