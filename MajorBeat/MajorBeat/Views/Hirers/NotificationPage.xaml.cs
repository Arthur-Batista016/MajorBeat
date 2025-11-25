using MajorBeat.Views.Users;

namespace MajorBeat.Views.Hirers;

public partial class NotificationPage : ContentPage
{
	public NotificationPage()
	{
		InitializeComponent();
	}

    private async void voltar_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HirerHomePage());
    }

    private async void OnNotifyTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProposalPageView());
    }


}