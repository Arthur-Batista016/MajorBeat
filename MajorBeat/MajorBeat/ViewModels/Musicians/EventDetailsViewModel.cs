using CommunityToolkit.Mvvm.ComponentModel;
using MajorBeat.Models;
using MajorBeat.Services.Users;

namespace MajorBeat.ViewModels.Musicians;

public partial class EventDetailsViewModel : ObservableObject
{
    EventService _eService = new EventService();

    [ObservableProperty]
    private long idEvento;

    [ObservableProperty]
    private Evento evento;

	public EventDetailsViewModel(long id)
	{
        IdEvento = id;
        _ = CarregarEvento();
    }
    



    public async Task CarregarEvento()
    {
        try
        {
            var eventoResultado = await _eService.GetEventById(IdEvento);

            // 2. FORCE a atualização a acontecer na Thread Principal
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Evento = eventoResultado;
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao carregar evento: {ex.Message}");
            // Você pode querer setar como nulo em caso de erro
            evento = null;
        }
    }
}