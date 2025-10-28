using MajorBeat.ViewModels.Users;

namespace MajorBeat.Views.Hirers;

public partial class MusicianDetails : ContentPage
{
	public MusicianDetails(long id_musico)
	{
		InitializeComponent();
		BindingContext = new DetailsViewModel(id_musico);
	}
}