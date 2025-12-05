using MajorBeat.Views.Hirers;
using MajorBeat.Views.Musicians;
using MajorBeat.Views.Users;

namespace MajorBeat
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Application.Current.UserAppTheme = AppTheme.Light;

            MainPage = new NavigationPage(new InitialPage());
        }
    }
}
