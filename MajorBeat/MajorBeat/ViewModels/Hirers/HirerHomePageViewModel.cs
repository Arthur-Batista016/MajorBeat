using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MajorBeat.Models;
using MajorBeat.Services.Musicians;
using MajorBeat.Services.Users;

namespace MajorBeat.ViewModels.Hirers
{
    public partial class HirerHomePageViewModel : ObservableObject
    {

        MusicianService _mService;
        
        private ObservableCollection<Musico> musicos;
        
        [ObservableProperty]
        private string currentToken;

        [ObservableProperty]
        private string marginGenero = "100";


        public HirerHomePageViewModel()
        {

            _mService = new MusicianService();
            currentToken = _mService.Token;
            ActualPosition = 0;
            EventPhoto = new ObservableCollection<string>();
            musicos = new ObservableCollection<Musico>();

            ExibirTodosMusicos();
        }

        public ICommand GerarCommand { get; set; }


        private ObservableCollection<string> eventPhoto;
        private int actualPosition;

        [ObservableProperty]
        public bool hasMusicians;

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

        public ObservableCollection<Musico> Musicos
        {
            get => musicos;
            set
            {
                musicos = value;
                OnPropertyChanged(nameof(Musicos));
                HasMusicians = musicos != null && musicos.Count > 0; // Atualiza HasEvent
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
            HasMusicians = Musicos != null && Musicos.Count > 0;
        }

        public async Task<ObservableCollection<Musico>> ExibirTodosMusicos()
        {
            try
            {
                
                ObservableCollection<Musico> musicos = await _mService.GetAllMusicians();
                Musicos = musicos;
                return Musicos;
               
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar musicos: {ex.Message}");
                return Musicos = new ObservableCollection<Musico>();
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
}
