using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Models;
using MajorBeat.Views.Musicians;
using System.Collections.ObjectModel;
using System.Globalization;

namespace MajorBeat.ViewModels.Musician;

public partial class CalendarDay : ObservableObject
{
    public DateTime Date { get; set; }
    public string DayOfMonth => IsEmptyDay ? string.Empty : Date.Day.ToString();

    [ObservableProperty]
    private bool hasEvent;

    public bool IsEmptyDay => Date == DateTime.MinValue;

    public Color BackgroundColor => HasEvent ? Color.FromArgb("#B19CD9") : Color.FromArgb("#FFFFFF"); // Lilás mais claro
    public Color TextColor => HasEvent ? Color.FromArgb("#4F1271") : Color.FromArgb("#000000"); // Roxo escuro
}

public partial class MusicianCalendarViewModel : ObservableObject
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

    [ObservableProperty]
    private ObservableCollection<CalendarDay> calendarDays;

    [ObservableProperty]
    private string currentMonthTitle;

    public MusicianCalendarViewModel()
    {
        Filtros = new ObservableCollection<string> { "Próximos", "Concluídos" };

        // Dados de mock
        Eventos = new ObservableCollection<Evento>
        {
            new Evento
            {
                idEvento = 1,
                nome = "Panelão do Norte",
                data = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 26), // Usando o dia atual (26)
                horaInicio = new TimeSpan(18, 0, 0),
                horaFim = new TimeSpan(21, 0, 0),
            },
            new Evento
            {
                idEvento = 2,
                nome = "Festival Harmoniar",
                data = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 10), // Usando o dia 10 do mês atual
                horaInicio = new TimeSpan(14, 0, 0),
                horaFim = new TimeSpan(23, 0, 0),
            },
            new Evento
            {
                idEvento = 3,
                nome = "Casamento",
                data = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 15), // Usando o dia 15 do mês atual
                horaInicio = new TimeSpan(19, 0, 0),
                horaFim = new TimeSpan(22, 0, 0),
            },
            // Evento no próximo mês
            new Evento
            {
                idEvento = 4,
                nome = "Aniversário",
                data = DateTime.Today.AddMonths(1).Date.AddDays(10 - DateTime.Today.AddMonths(1).Day),
                horaInicio = new TimeSpan(19, 0, 0),
                horaFim = new TimeSpan(22, 0, 0),
            }
        };

        foreach (var evento in Eventos)
        {
            if (evento.data.HasValue)
                UnavailableDates.Add(evento.data.Value.Date);
        }
        // Armazenar apenas a data, sem hora

        EventosFiltrados = new ObservableCollection<Evento>();
        CalendarDays = new ObservableCollection<CalendarDay>();

        FiltroSelecionado = "Próximos";
        GenerateCalendar();
    }

    partial void OnFiltroSelecionadoChanged(string value)
    {
        FiltrarEventos();
    }

    // Chamado quando a propriedade CurrentMonth muda (Ex: NextMonthCommand)
    partial void OnCurrentMonthChanged(DateTime value)
    {
        GenerateCalendar();
    }

    private void FiltrarEventos()
    {
        if (Eventos == null || EventosFiltrados == null)
            return;

        var eventosParaExibir =
    FiltroSelecionado == "Concluídos"
    ? Eventos.Where(e => e.data.HasValue && e.data.Value.Date < DateTime.Today)
    : Eventos.Where(e => e.data.HasValue && e.data.Value.Date >= DateTime.Today);


        EventosFiltrados.Clear();
        foreach (var evento in eventosParaExibir.OrderBy(e => e.data))
        {
            EventosFiltrados.Add(evento);
        }
    }

    private void GenerateCalendar()
    {
        CalendarDays.Clear();

        // Formata o título do mês: "Novembro 2025"
        CurrentMonthTitle = CurrentMonth.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("pt-BR")).ToUpperInvariant();

        var firstDayOfMonth = new DateTime(CurrentMonth.Year, CurrentMonth.Month, 1);
        var daysInMonth = DateTime.DaysInMonth(CurrentMonth.Year, CurrentMonth.Month);

        // O dia da semana no .NET (DayOfWeek) começa em Domingo (0). 
        // O calendário MAUI geralmente começa em Segunda-feira.
        // Mapeia de volta para 0=Segunda, 1=Terça...
        var startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;
        // Ajusta o índice para começar em Segunda (1) para evitar um dia vazio se o mês começar em Domingo
        var startDayIndex = (startDayOfWeek == 0) ? 6 : startDayOfWeek - 1;

        // Adiciona dias vazios no início do mês (preenchimento)
        for (int i = 0; i < startDayIndex; i++)
        {
            CalendarDays.Add(new CalendarDay { Date = DateTime.MinValue, HasEvent = false });
        }

        // Adiciona os dias reais do mês
        for (int day = 1; day <= daysInMonth; day++)
        {
            var date = new DateTime(CurrentMonth.Year, CurrentMonth.Month, day);
            var hasEvent = UnavailableDates.Any(d => d.Date == date.Date);

            CalendarDays.Add(new CalendarDay
            {
                Date = date,
                HasEvent = hasEvent
            });
        }
    }

    [RelayCommand]
    private void NextMonth()
    {
        CurrentMonth = CurrentMonth.AddMonths(1);
    }

    [RelayCommand]
    private void PreviousMonth()
    {
        CurrentMonth = CurrentMonth.AddMonths(-1);
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