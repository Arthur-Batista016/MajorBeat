using MajorBeat.Models.Enums;
using MajorBeat.ViewModels.Hirer;

namespace MajorBeat.Views.Hirers;

public partial class CreateEventPageView : ContentPage
{
	public CreateEventPageView()
	{
		InitializeComponent();
		BindingContext= new CreateEventPageViewModel();
    }


    private void OnInstrumentosSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BindingContext is CreateEventPageViewModel viewModel)
        {
            // Remove instrumentos que foram desmarcados
            foreach (InstrumentoEnum removido in e.PreviousSelection.Except(e.CurrentSelection))
                viewModel.InstrumentosSelecionados.Remove(removido);

            // Adiciona novos instrumentos selecionados
            foreach (InstrumentoEnum adicionado in e.CurrentSelection.Except(e.PreviousSelection))
                viewModel.InstrumentosSelecionados.Add(adicionado);
        }
    }

    private void OnGenerosSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BindingContext is CreateEventPageViewModel viewModel)
        {
            // Remove gêneros que foram desmarcados
            foreach (GeneroEnum removido in e.PreviousSelection.Except(e.CurrentSelection))
                viewModel.GenerosSelecionados.Remove(removido);

            // Adiciona novos gêneros selecionados
            foreach (GeneroEnum adicionado in e.CurrentSelection.Except(e.PreviousSelection))
                viewModel.GenerosSelecionados.Add(adicionado);
        }
    }
}