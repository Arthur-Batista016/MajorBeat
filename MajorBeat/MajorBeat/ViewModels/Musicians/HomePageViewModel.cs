using CommunityToolkit.Mvvm.ComponentModel;
using MajorBeat.Enums;
using MajorBeat.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MajorBeat.ViewModels.Musicians
{
    public class HomePageViewModel : BaseViewModel
    {
        private ObservableCollection<string> eventPhoto;
        private int actualPosition;
       
        private ObservableCollection<Evento> eventos;

        public ObservableCollection<Evento> Eventos
        {
            get => eventos;
            set
            {
                eventos = value;
                OnPropertyChanged(nameof(Evento));
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
            CriarCommand = new Command(async () => await EventoPadrao());
        }

        public ICommand CriarCommand { get; set; }

        public async Task EventoPadrao()
        {
            var evento = new Evento()
            {
                IdEvento = 1,
                Nome = "Panelão do Norte",
                TipoEvento = Enums.TipoEvento.BAR,
                Data = new DateTime(2025, 12, 15),
                NomeGenero = new ObservableCollection<NomeGenero> { Enums.NomeGenero.SERTANEJO },
                Avaliacoes = new ObservableCollection<Avaliacao>
            {
              new Avaliacao { nota = 4.2 }
            }
            };
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
