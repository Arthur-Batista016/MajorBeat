using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MajorBeat.Enums;
using MajorBeat.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MajorBeat.ViewModels.Musicians
{
    public partial class HomePageViewModel : ObservableObject
    {
        private ObservableCollection<string> eventPhoto;
        private int actualPosition;
       
        private ObservableCollection<Evento> eventos;

        [ObservableProperty]
        public bool hasEvent;
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

        public HomePageViewModel()
        {
            // Inicializa o Evento com valores exemplo

            Eventos = new ObservableCollection<Evento>();
            EventoPadrao();
            ChangeEventPhoto();
            ActualPosition = 0;
            CriarCommand = new Command(async () => { await EventoPadrao(); await EventsIsEmpyty(); });
        }

        public ICommand CriarCommand { get; set; }
        public ICommand EnviarCommand { get; set; }


        public async Task EventsIsEmpyty()
        {
            if (Eventos.Count == 0)
                HasEvent = true;
            else
                HasEvent = false;
        }
        
        public async Task EventoPadrao()
        {

            var evento = new Evento()
            {
                Nome = namevent,
                TipoEvento = Enums.TipoEvento.BAR,
                Data = new DateTime(2025, 12, 15),
                NomeGenero = new ObservableCollection<NomeGenero> { Enums.NomeGenero.SERTANEJO },
                Avaliacoes = new ObservableCollection<Avaliacao>
            {
              new Avaliacao { nota = 4.2 }
            }
            };

            Eventos.Add(evento);
          
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

        public async Task Filters()
        {
         
        }
    }
}
