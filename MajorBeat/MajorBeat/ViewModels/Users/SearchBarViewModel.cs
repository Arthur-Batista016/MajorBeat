using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public SearchBarViewModel()
        {
            
        }

        public async Task onFocus()
        {
            BarBackground = "#E7E7E7";
            BarFormat = new RoundRectangle { CornerRadius = new CornerRadius(10, 10, 0, 0) };
            BarVisibility = true;

        }

        public async Task onUnfocus()
        {
            BarBackground = "#AE92BD";
            BarFormat = new RoundRectangle { CornerRadius = new CornerRadius(10, 10, 10, 10) };
            BarVisibility = false;
        }

    }
}
