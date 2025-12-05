using MajorBeat.ViewModels.Users;

namespace MajorBeat.Views.Users;

public partial class ProposalPageView : ContentPage
{
	public ProposalPageView(long id_proposta)
	{
		InitializeComponent();
		BindingContext = new ProposalDetailsViewModel(id_proposta);
       
    }
}