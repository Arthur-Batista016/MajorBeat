using MajorBeat.ViewModels.Hirer;

namespace MajorBeat.Views.Users;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();
    }
}