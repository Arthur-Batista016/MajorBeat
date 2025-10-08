using MajorBeat.Views.Musicians;
using MajorBeat.Views.Users;

namespace MajorBeat
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MusicianSearchPage();
        }
    }
}
