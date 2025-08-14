namespace MajorBeat.Views;

public partial class HirerRegisterPage : ContentPage
{
	public HirerRegisterPage()
	{
		InitializeComponent();
	}

    private async void voltar_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InitialPage());
    }


}