using MajorBeat.Views.Musicians;

namespace MajorBeat
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
