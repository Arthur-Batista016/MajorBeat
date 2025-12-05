using CommunityToolkit.Mvvm.ComponentModel;
using MajorBeat.Models;
using MajorBeat.Services.Users;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Musicians;

public partial class EventDetailsViewModel : ObservableObject
{
    EventService _eService = new EventService();

    [ObservableProperty]
    private long idEvento;

    [ObservableProperty]
    private Evento evento;

    private readonly PropostaService _pService = new PropostaService();

    [ObservableProperty]
    private bool isProposalSend = false;

    [ObservableProperty]
    private Proposta proposta;


    [ObservableProperty]
    private string valorProposta;
    public ICommand ProposalCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand SendCommand { get; }

    private long idMusico;
    private string token;
    public EventDetailsViewModel(long id)
    {
        token = Preferences.Get("UsuarioToken", string.Empty);
        _pService = new PropostaService(token);
        idMusico = Preferences.Get("Usuarioid", 0L);
        IdEvento = id;
        _ = CarregarEvento();
        ProposalCommand = new Command(async () => await StartProposal());
        CancelCommand = new Command(async () => await CancelProposalSend());
        SendCommand = new Command(async () => await SendProposal());
    }
    [ObservableProperty]
    private bool messageVisibility = true;

    public async Task StartProposal()
    {
        IsProposalSend = true;
        MessageVisibility = false;
    }

    public async Task CancelProposalSend()
    {
        IsProposalSend = false;
        MessageVisibility = true;
    }

    public async Task<Proposta> SendProposal()
    {
        try
        {
            if (Evento == null)
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Por favor, selecione um Evento para enviar a proposta.", "OK");
                return null;
            }
            IsProposalSend = false;
            MessageVisibility = true;
            string valorFormatado = ValorProposta;

            // 1. Remove R$, pontos, e substitui vírgula por ponto (para padrão API)
            string valorLimpo = valorFormatado
                .Replace("R$", "")
                .Replace(".", "") // Remove separadores de milhar (pontos)
                .Replace(",", "."); // Troca vírgula por ponto (separador decimal)

            if (double.TryParse(valorLimpo, System.Globalization.NumberStyles.Currency,
                                System.Globalization.CultureInfo.InvariantCulture, out double valorNumerico))
            {
                Proposta p = new Proposta();
                // p.evento = new Evento(); // 🚨 REMOVER ESTA LINHA: Desnecessária para o POST
                p.idRecebedor = Evento.contratante.idContratante;

                // ✅ CORREÇÃO AQUI: Atribua o valor double convertido
                p.valor = valorNumerico;

                p.idEvento = IdEvento;

                await _pService.PostPropostaAsync(p);
                await Application.Current.MainPage.DisplayAlert(
                                    "Sucesso!",
                                    "Proposta Enviada Com Sucesso para o Contratante!",
                                    "OK"
                                );

                Proposta = p;
                return Proposta; // Retorna a proposta em caso de sucesso
            }
            else
            {
                // Caso o TryParse falhe (usuário digitou algo não numérico)
                await Application.Current.MainPage.DisplayAlert("Erro de Conversão", "O valor da proposta não é um número válido.", "OK");
                return null;
            }
        }
        catch (Exception ex)
        {
            // Trate erros de API ou rede
            await Application.Current.MainPage.DisplayAlert("Erro ao Enviar", ex.Message, "OK");
            return null;
        }
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