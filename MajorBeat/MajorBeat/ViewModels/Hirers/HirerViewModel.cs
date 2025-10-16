using CommunityToolkit.Mvvm.ComponentModel;

using MajorBeat.Enums;
using MajorBeat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Hirers
{
    public partial class HirerViewModel:ObservableObject
    {
        private ObservableCollection<string> eventPhoto;
        
        private int actualPosition;

        [ObservableProperty]
        private ObservableCollection<Musico> musicos;

        [ObservableProperty]
        public bool hasEvent;

        [ObservableProperty]
        private string musicianName;

        [ObservableProperty]
        private ObservableCollection<NomeGenero> generos;

        [ObservableProperty]
        private Avaliacao avaliacao;

        
     
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

        public HirerViewModel()
        {
            // Inicializa o Evento com valores exemplo

            Musicos = new ObservableCollection<Musico>();
            ChangeEventPhoto();
            ActualPosition = 0;
            CriarCommand = new Command(async () => { await EventoPadrao(); await EventsIsEmpyty(); });
        }

        public ICommand CriarCommand { get; set; }
        public ICommand EnviarCommand { get; set; }








        public async Task EventsIsEmpyty()
        {
            HasEvent = musicos.Count > 0;
        }

        public async Task EventoPadrao()
        {
            ChangeEventPhoto();
            var musico = new Musico()
            {
                nome = MusicianName,
                avaliacoes = new ObservableCollection<Avaliacao> {
                new Avaliacao { nota = 4.2 }
            },
                nomeGenero = new ObservableCollection<NomeGenero> { Enums.NomeGenero.SERTANEJO, Enums.NomeGenero.AXE }
            };

           Musicos.Add(musico);

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
