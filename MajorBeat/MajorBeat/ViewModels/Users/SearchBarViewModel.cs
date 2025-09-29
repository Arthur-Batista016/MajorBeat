using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics.Text;

namespace MajorBeat.ViewModels.Users
{
    public partial class SearchBarViewModel:ObservableObject
    {
        [ObservableProperty]
        public bool barVisibility = false;

        [ObservableProperty]
        public RoundRectangle barFormat = new RoundRectangle { CornerRadius = new CornerRadius(10, 10, 10, 10) };

        [ObservableProperty]
        public string barBackground = "#AE92BD";

        [ObservableProperty]
        public Color placeholderColor = Color.FromArgb("#FFFFFF");

        [ObservableProperty]
        public int heightNewItem = 50;

        [ObservableProperty]
        public Color textColor = Color.FromArgb("#FFFFFF");

        [ObservableProperty]
        public string lupa = "lupainverted.png";

        [ObservableProperty]
        public string userEntry;

        [ObservableProperty]
        public ObservableCollection<String> searchs;



        public ICommand SearchCommand { get; set; }

        public SearchBarViewModel()
        {
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            Searchs = new ObservableCollection<string>();
            SearchCommand = new Command(async () => await search());
         
        }



        public async Task onFocus()
        {
            BarBackground = "#E7E7E7";
            BarFormat = new RoundRectangle { CornerRadius = new CornerRadius(10, 10, 0, 0) };
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

        }

        public async Task recentSearchs()
        {
            if(Searchs.Count() != 0 && UserEntry != "") {
                for (int i = 0; i < Searchs.Count(); i++) {
                    heightNewItem += 50;
                    heightNewItem.ToString();

                    }
            }
            
        }

        public async Task search()
        {
            if (UserEntry != "")
            {
                Searchs.Add(userEntry);
            }
        }

    }
}
