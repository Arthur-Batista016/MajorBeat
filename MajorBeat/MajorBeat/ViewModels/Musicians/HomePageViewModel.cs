using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Musicians
{
    public class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<string> eventPhoto;


        public int actualPosition;

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
            ChangeEventPhoto();
            ActualPosition = 0;
        }

        public async Task ChangeEventPhoto()
        {
            EventPhoto = new ObservableCollection<string>()
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
