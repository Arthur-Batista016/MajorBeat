using MajorBeat.ViewModels.Hirers;

namespace MajorBeat.Views.Hirers;

public partial class HirerCreateProfileView : ContentPage
{
	public HirerCreateProfileView(HirerCreateProfileViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
    private async void voltar_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserRegisterView());
    }
}