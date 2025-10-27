using MajorBeat.ViewModels.Hirer;

namespace MajorBeat.Views.Users;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }

    private async void voltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InitialPage());
    }
}