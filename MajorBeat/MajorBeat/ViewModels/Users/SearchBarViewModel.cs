using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Enums;
using MajorBeat.Models;
using MajorBeat.Services.Users;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics.Text;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Users
{
    public partial class SearchBarViewModel : ObservableObject
    {


        EventService _eService;

        [ObservableProperty]
        ObservableCollection<Evento> eventos;



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
        NomeGenero.AXE,
        NomeGenero.CLASSICO,
        NomeGenero.ELETRONICO,
        NomeGenero.FUNK,
        NomeGenero.RAP,           // Hip-Hop/Rap
        NomeGenero.JAZZ,
        NomeGenero.POP,
        NomeGenero.SAMBA,
        NomeGenero.BLUES,
        NomeGenero.FORRO,
        NomeGenero.GOSPEL,
        NomeGenero.TRAP,          // Infantil (você pode criar um enum separado se quiser)
        NomeGenero.METAL,
        NomeGenero.ROCK,
        NomeGenero.SERTANEJO
        };





        public ICommand SearchCommand { get; set; }

        public SearchBarViewModel()
        {
            InicializarCommands();
            onUnfocus();
            _eService = new EventService();

            AxeCommand = new Command(async () => await AxeChoosed());
            BluesCommand = new Command(async () => await BluesChoosed());
            ClassicoCommand = new Command(async () => await ClassicoChoosed());
            DiscoCommand = new Command(async () => await DiscoChoosed());
            EletronicoCommand = new Command(async () => await EletronicoChoosed());
            ForroCommand = new Command(async () => await ForroChoosed());
            FunkCommand = new Command(async () => await FunkChoosed());
            GospelCommand = new Command(async () => await GospelChoosed());
            HipHopCommand = new Command(async () => await HipHopChoosed());
            InfantilCommand = new Command(async () => await InfantilChoosed());
            JazzCommand = new Command(async () => await JazzChoosed());
            MetalCommand = new Command(async () => await MetalChoosed());
            PopCommand = new Command(async () => await PopChoosed());
            RockCommand = new Command(async () => await RockChoosed());
            SambaCommand = new Command(async () => await SambaChoosed());
            SertanejoCommand = new Command(async () => await SertanejoChoosed());
            OutroCommand = new Command(async () => await OutroChoosed());






        }

        public void InicializarCommands()
        {
            Searchs = new ObservableCollection<string>();
            SearchCommand = new Command(async () => await search());

         
        }


        //METODOS DE PESQUISA
        public async Task onFocus()
        {
            if (Searchs.Count() == 0)
            {
               BarFormat = new RoundRectangle { CornerRadius = new CornerRadius(10, 10, 10, 10) };
            }
            else if(Searchs.Count() >0 )
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
            if(Searchs.Count() != 0) {
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




            //METODOS HIRER SEARCH PAGE]



        //Hirer Search Page
        
        [ObservableProperty]
        public bool isSearch = false;

        [ObservableProperty]
        public bool findResults = true;


        public ICommand AxeCommand { get; set; }
        public ICommand BluesCommand { get; set; }
        public ICommand ClassicoCommand { get;  set; }
        public ICommand DiscoCommand { get; set; }
        public ICommand EletronicoCommand { get;set; }
        public ICommand ForroCommand { get; set; }
        public ICommand FunkCommand { get; set; }
        public ICommand GospelCommand { get;  set; }
        public ICommand HipHopCommand { get; set; }
        public ICommand InfantilCommand { get;  set; }
        public ICommand JazzCommand { get;set ; }
        public ICommand MetalCommand { get;set; }
        public ICommand PopCommand { get; set; }
        public ICommand RockCommand { get;  set; }
        public ICommand SambaCommand { get;  set; }
        public ICommand SertanejoCommand { get;  set; }
        public ICommand OutroCommand { get; set; }

        public async Task GenreChoosed()
        {
           
            IsSearch = true;
            FindResults = false;
        }

        public async Task<ObservableCollection<Evento>> AxeChoosed()
        {
            try
            {
                ObservableCollection<Evento> evento = await _eService.GetEventsByGenre(Enums.NomeGenero.AXE);
                if (evento.Count.Equals(0))
                {

                }
                else
                {
                    IsSearch = true;
                    FindResults = false;
              

                }

                return evento;
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage
                   .DisplayAlert("Erro", ex.Message, "OK");
                return new ObservableCollection<Evento>();
            }
              
            
        }

        public async Task BluesChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }

        public async Task ClassicoChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }

        public async Task DiscoChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }

        public async Task EletronicoChoosed()
        {
            isSearch = true;
            FindResults = false;
        }

        public async Task ForroChoosed()
        {
            IsSearch = true;
            FindResults = false;

        }

        public async Task FunkChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }

        public async Task GospelChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }

        public async Task HipHopChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }

        public async Task InfantilChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }

        public async Task JazzChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }
        public async Task MetalChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }

        public async Task PopChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }
        public async Task RockChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }

        public async Task SambaChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }

        public async Task SertanejoChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }

        public async Task OutroChoosed()
        {
            IsSearch = true;
            FindResults = false;
        }


    }
}
