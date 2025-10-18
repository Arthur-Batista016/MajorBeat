using MajorBeat.ViewModels.Hirers;
using MajorBeat.ViewModels.Users;

namespace MajorBeat.Views.Hirers;

public partial class HirerHomePage : ContentPage
{

    public HirerHomePage()
    {
        InitializeComponent();
        BindingContext = new HirerViewModel();

      
    }

    private async void searchBar_Focused(object sender, FocusEventArgs e)
    {
        
    }

    private void searchBar_Unfocused(object sender, FocusEventArgs e)
    {
   
    }
}
