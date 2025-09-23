using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Musicians
{
    public class HomePageViewModel:BaseViewModel
    {
        public ObservableCollection<string> eventPhoto;

        public ObservableCollection<string> EventPhoto
        {
            get => eventPhoto;
            set
            {
                eventPhoto = value;
                OnPropertyChanged(nameof(EventPhoto));
            }
        }




        public ICommand changeImageCommand;

        public HomePageViewModel()
        {
            changeEventPhoto();
            
        }

       

        public async Task changeEventPhoto()
        {
            string actualPhoto = "1/6";
            int next = 1;

            EventPhoto = new ObservableCollection<string>()
            {
                "panelao.png",
                "birthday.png",
                "bar.png"
            };


            



        }

        public async Task filters()
        {

        }
    }
}
