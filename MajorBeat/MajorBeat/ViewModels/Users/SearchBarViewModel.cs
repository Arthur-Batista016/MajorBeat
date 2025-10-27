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

        MusicianService _mService;

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
                    SearchByLetters();
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

        private ObservableCollection<Musico> filteredMusicos = new ObservableCollection<Musico>();
        public ObservableCollection<Musico> FilteredMusicos
        {
            get => filteredMusicos;
            set
            {
                filteredMusicos = value;
                OnPropertyChanged(nameof(FilteredMusicos));
            }
        }

        [ObservableProperty]
        public bool recentSearch = false;

        [ObservableProperty]
        public ObservableCollection<string> searchs;

        public ICommand SearchCommand { get; set; }

        public SearchBarViewModel()
        {
            InicializarCommands();
            onUnfocus();
            _mService = new MusicianService();
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
            BarSearch = true;
            HirerNoSelect = false;
        }

        public async Task<ObservableCollection<Musico>> SearchByLetters()
        {
            if (string.IsNullOrWhiteSpace(UserEntry))
            {
                FilteredSearchs = new ObservableCollection<string>(Searchs);
                FilteredMusicos = new ObservableCollection<Musico>(Musicos);
                return FilteredMusicos;
            }
            else
            {
                var searchLower = UserEntry.ToLowerInvariant();

                // Filtra pesquisas recentes
                var filtrados = Searchs
                    .Where(i => i.ToLowerInvariant().Contains(searchLower))
                    .ToList();
                FilteredSearchs = new ObservableCollection<string>(filtrados);

                // Filtra músicos
                var musicosFiltrados = Musicos
                    .Where(m => m.nome.ToLowerInvariant().Contains(searchLower))
                    .ToList();
                return FilteredMusicos = new ObservableCollection<Musico>(musicosFiltrados);
            }
        }



        //METODOS HIRER SEARCH PAGE]



        //Hirer Search Page

        // Imagens fixas para cada gênero
        public string AxeImage => "axe.png";
        public string BluesImage => "blues.png";
        public string ClassicoImage => "classico.png";
        public string DiscoImage => "disco.png";
        public string EletronicoImage => "eletronico.png";
        public string ForroImage => "forro.png";
        public string FunkImage => "funk.png";
        public string GospelImage => "gospel.png";
        public string RapImage => "hiphop.png";
        public string TrapImage => "infantil.png";
        public string JazzImage => "jazz.png";
        public string MetalImage => "metal.png";
        public string PopImage => "pop.png";
        public string RockImage => "rock.png";
        public string SambaImage => "samba.png";
        public string SertanejoImage => "sertanejo.png";
        public string OutroImage => "outro.png";
        /// 


        [ObservableProperty]
        public bool hirerNoSelect = true;

        [ObservableProperty]
        public bool isSearch = false;

        [ObservableProperty]
        public bool barSearch = false;

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
            BarSearch = false;
            Musicos = new ObservableCollection<Musico>();
        }


        


        private async Task<ObservableCollection<Musico>> BuscarPorGenero(NomeGenero genero)
        {
            try
            {
                ObservableCollection<Musico> resultado = await _mService.GetMusicianByGenre(genero);
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
