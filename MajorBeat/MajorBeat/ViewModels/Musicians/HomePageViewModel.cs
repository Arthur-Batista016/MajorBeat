using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using MajorBeat.Enums;
using MajorBeat.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MajorBeat.ViewModels.Musicians
{
    public class HomePageViewModel : BaseViewModel
    {
        private ObservableCollection<string> eventPhoto;
        private int actualPosition;
        private Evento evento;

        public Evento Evento
        {
            get => evento;
            set
            {
                evento = value;
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
          
            
            ChangeEventPhoto();
            ActualPosition = 0;
        }

        public async Task EventoPadrao()
        {
           
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
