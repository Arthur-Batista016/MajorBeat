namespace MajorBeat.Views.Musicians;

using MajorBeat.ViewModels.Users;
public partial class MusicianSearchPage : ContentPage
{
    private SearchBarViewModel Vm => BindingContext as SearchBarViewModel;
    public MusicianSearchPage()
	{
		InitializeComponent();
        BindingContext = new SearchBarViewModel();
	}

    private void searchBar_Focused(object sender, FocusEventArgs e)
    {
        Vm?.onFocus();
    }

    private void searchBar_Unfocused(object sender, FocusEventArgs e)
    {
        Vm.onUnfocus();
    }
}