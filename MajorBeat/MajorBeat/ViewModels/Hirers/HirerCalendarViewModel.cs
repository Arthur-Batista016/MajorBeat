using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Models;
using MajorBeat.Views.Hirers;
using MajorBeat.Views.Musicians;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;


namespace MajorBeat.ViewModels.Hirer;

public partial class HirerCalendarViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Evento> eventos;

    [ObservableProperty]
    private ObservableCollection<Evento> eventosFiltrados;

    [ObservableProperty]
    private ObservableCollection<string> filtros;

    [ObservableProperty]
    private ObservableCollection<DateTime> unavailableDates = new();

    [ObservableProperty]
    private string filtroSelecionado;

    [ObservableProperty]
    private DateTime currentMonth = DateTime.Today;

    public HirerCalendarViewModel()
    {
        Filtros = new ObservableCollection<string> { "Próximos", "Concluídos" };

        Eventos = new ObservableCollection<Evento>
        {
            new Evento
            {
                idEvento = 1,
                nome = "Panelão do Norte",
                data = new DateTime(2025, 10, 30),
                horaInicio = new TimeSpan(18, 0, 0),
                horaFim = new TimeSpan(21, 0, 0),
            },
            new Evento
            {
                idEvento = 2,
                nome = "Festival Harmoniar",
                data = new DateTime(2025, 11, 10),
                horaInicio = new TimeSpan(14, 0, 0),
                horaFim = new TimeSpan(23, 0, 0),
            },
            new Evento
            {
                idEvento = 3,
                nome = "Casamento",
                data = new DateTime(2025, 9, 15),
                horaInicio = new TimeSpan(19, 0, 0),
                horaFim = new TimeSpan(22, 0, 0),
            }
        };

        foreach (var evento in Eventos)
            UnavailableDates.Add(evento.data);

        EventosFiltrados = new ObservableCollection<Evento>();

        FiltroSelecionado = "Próximos";

    }

    partial void OnFiltroSelecionadoChanged(string value)
    {
        FiltrarEventos();
    }

    private void FiltrarEventos()
    {
        if (Eventos == null || EventosFiltrados == null)
            return;

        var eventosParaExibir = FiltroSelecionado == "Concluídos"
            ? Eventos.Where(e => e.data < DateTime.Today)
            : Eventos.Where(e => e.data >= DateTime.Today);

        EventosFiltrados.Clear();
        foreach (var evento in eventosParaExibir)
        {
            EventosFiltrados.Add(evento);
        }
    }



    [RelayCommand]
    private void AbrirOpcoes(Evento e)
    {
        if (e == null) return;

        App.Current.MainPage.DisplayActionSheet(
            $"Evento: {e.nome}",
            "Cancelar",
            null,
            "Editar Evento",
            "Excluir Evento");
    }

    [RelayCommand]
    private async Task AbrirDetalhesEvento(Evento evento)
    {
        if (evento == null) return;

        await Shell.Current.Navigation.PushAsync(new EventDetails(evento.idEvento));
    }



}