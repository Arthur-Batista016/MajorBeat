using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Models.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels
{
    public partial class CreateEventPageViewModel : ObservableObject
    {
        private string nomeEvento = string.Empty;
        public string NomeEvento
        {
            get => nomeEvento;
            set => SetProperty(ref nomeEvento, value);
        }

        private DateTime dataEvento = DateTime.Today;
        public DateTime DataEvento
        {
            get => dataEvento;
            set => SetProperty(ref dataEvento, value);
        }

        private string categoriaSelecionada = string.Empty;
        public string CategoriaSelecionada
        {
            get => categoriaSelecionada;
            set => SetProperty(ref categoriaSelecionada, value);
        }

        private string descricaoEvento = string.Empty;
        public string DescricaoEvento
        {
            get => descricaoEvento;
            set => SetProperty(ref descricaoEvento, value);
        }

        public ObservableCollection<string> Categorias { get; } = new()
        {
            "Aniversário", "Casamento", "Show", "Corporativo", "Outros"
        };

        public ObservableCollection<InstrumentoEnum> ListaInstrumentos { get; }

        public ObservableCollection<InstrumentoEnum> InstrumentosSelecionados { get; } = new();

        private string textoBuscaInstrumento = string.Empty;
        public string TextoBuscaInstrumento
        {
            get => textoBuscaInstrumento;
            set => SetProperty(ref textoBuscaInstrumento, value);
        }

        public ICommand CriarEventoCommand { get; }

        public CreateEventPageViewModel()
        {
            ListaInstrumentos = new ObservableCollection<InstrumentoEnum>(
                Enum.GetValues(typeof(InstrumentoEnum)).Cast<InstrumentoEnum>()
            );

            CriarEventoCommand = new AsyncRelayCommand(CriarEventoAsync);
        }

        private async Task CriarEventoAsync()
        {
            if (string.IsNullOrWhiteSpace(NomeEvento))
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Informe o nome do evento.", "OK");
                return;
            }

            string resumo = $"Evento: {NomeEvento}\nData: {DataEvento:d}\nCategoria: {CategoriaSelecionada}\n" +
                            $"Instrumentos: {string.Join(", ", InstrumentosSelecionados)}\nDescrição: {DescricaoEvento}";

            await App.Current.MainPage.DisplayAlert("Evento criado!", resumo, "OK");

            NomeEvento = string.Empty;
            CategoriaSelecionada = string.Empty;
            DescricaoEvento = string.Empty;
            InstrumentosSelecionados.Clear();
            DataEvento = DateTime.Today;
        }
    }
}
