using MajorBeat.Enums;
using MajorBeat.Services;
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
            foreach (NomeInstrumento removido in e.PreviousSelection.Except(e.CurrentSelection))
                viewModel.InstrumentosSelecionados.Remove(removido);

            // Adiciona novos instrumentos selecionados
            foreach (NomeInstrumento adicionado in e.CurrentSelection.Except(e.PreviousSelection))
                viewModel.InstrumentosSelecionados.Add(adicionado);
        }
    }

    private void OnGenerosSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BindingContext is CreateEventPageViewModel viewModel)
        {
            // Remove gêneros que foram desmarcados
            foreach (NomeGenero removido in e.PreviousSelection.Except(e.CurrentSelection))
                viewModel.GenerosSelecionados.Remove(removido);

            // Adiciona novos gêneros selecionados
            foreach (NomeGenero adicionado in e.CurrentSelection.Except(e.PreviousSelection))
                viewModel.GenerosSelecionados.Add(adicionado);
        }
    }
}