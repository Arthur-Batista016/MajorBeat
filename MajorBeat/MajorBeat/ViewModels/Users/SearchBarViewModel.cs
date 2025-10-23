using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Enums;
using MajorBeat.Models;
using MajorBeat.Services.Musicians;
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
        MusicianService _mService;

      

        [ObservableProperty]
        ObservableCollection<Evento> eventos;

        [ObservableProperty]
        ObservableCollection<Musico> musicos;





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
            _mService = new MusicianService();
            _eService = new EventService();

            GeneroCommand = new AsyncRelayCommand<NomeGenero>(BuscarPorGenero);
            BackGenreCommand = new Command(async () => await BackGenreChoosed());







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
        public bool hirerNoSelect = true;

        [ObservableProperty]
        public bool isSearch = false;

        [ObservableProperty]
        public bool findResults = true;

        [ObservableProperty]
        public bool noMusics = false;



        public ICommand GeneroCommand { get; set; }
        public ICommand BackGenreCommand { get; set; }

        public async Task BackGenreChoosed()
        {
            HirerNoSelect = true;
            IsSearch = false;
            FindResults = false;
            NoMusics = false;
            Musicos = new ObservableCollection<Musico>();
        }


        


        private async Task<ObservableCollection<Musico>> BuscarPorGenero(NomeGenero genero)
        {
            try
            {
                var resultado = await _mService.GetMusicianByGenre(genero);
                Musicos = resultado;

                if (Musicos?.Count > 0)
                {
                  
                    HirerNoSelect = false;
                    IsSearch = true;
                    FindResults = true;
                    NoMusics = false;

                    
                }
               
                return Musicos;
            }
            catch (Exception ex)
            {
                HirerNoSelect = false;
                NoMusics = true;
                IsSearch = false;
                FindResults = false;
                return Musicos = new ObservableCollection<Musico>();
            }
        }

    }
}
