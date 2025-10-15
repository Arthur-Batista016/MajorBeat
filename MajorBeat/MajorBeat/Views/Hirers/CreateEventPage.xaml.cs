using System.Collections.ObjectModel;
using MajorBeat.Models.Enums;
using MajorBeat.ViewModels;
namespace MajorBeat.Views.Hirers;

public partial class CreateEventPage : ContentPage
{
    public CreateEventPage()
    {
        InitializeComponent();
        EventDatePicker.MinimumDate = DateTime.Today;

    }

    private async void OnCategoryChosen(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        if (picker.SelectedIndex < 0) return;

        // pega o texto do item (quando usamos Items)
        var selected = picker.Items[picker.SelectedIndex];

        await DisplayAlert("Categoria", $"Você escolheu: {selected}", "OK");
    }


    private ObservableCollection<string> filteredInstruments = new();
    private ObservableCollection<string> selectedInstruments = new();

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



}