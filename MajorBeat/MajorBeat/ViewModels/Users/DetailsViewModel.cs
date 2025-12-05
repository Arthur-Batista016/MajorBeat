
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Models;
using MajorBeat.Services.Musicians;
using MajorBeat.Services.Users;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Users
{
    public partial class DetailsViewModel : ObservableObject
    {
        private readonly PropostaService _pService = new PropostaService();
        private readonly MusicianService _mService = new MusicianService();
        private readonly EventService _eService = new EventService();

        // ---------------------------------------------------------
        // PROPRIEDADES DO MÚSICO
        // ---------------------------------------------------------

        [ObservableProperty]
        private long idMusico;

        [ObservableProperty]
        private Musico musico;

        // Controle de visibilidade da tela principal
        [ObservableProperty]
        private bool messageVisibility = true;

        private long usuarioId;
        private string token;

        [ObservableProperty]
        private Proposta proposta;

        [ObservableProperty]
        private ObservableCollection<Evento> eventosDisponiveis;
        private string role;

        // ---------------------------------------------------------
        // CONSTRUTOR
        // ---------------------------------------------------------
        public DetailsViewModel(long id)
        {
            role = Preferences.Get("role", string.Empty);
            IdMusico = id;
            _ = CarregarMusico();
            usuarioId = Preferences.Get("Usuarioid", 0L);
            token = Preferences.Get("UsuarioToken", string.Empty);
            _pService = new PropostaService(token);
            _eService = new EventService(token);
            EventosDisponiveis = new ObservableCollection<Evento>();
            _ = CarregarEventosContratante(usuarioId);


            // Inicialização dos comandos
            ProposalCommand = new Command(async () => await StartProposal());
            CancelCommand = new Command(async () => await CancelProposalSend());
            SendCommand = new Command(async () => await SendProposal());
        }


        // ---------------------------------------------------------
        // SESSÃO DE ABA SELECIONADA
        // ---------------------------------------------------------

        [ObservableProperty]
        private string selectedTab = "Historico";

        public bool IsHistoricoVisible => SelectedTab == "Historico";
        public bool IsSobreVisible => SelectedTab == "Sobre";
        public bool IsAvaliacoesVisible => SelectedTab == "Avaliacoes";

        partial void OnSelectedTabChanged(string value)
        {
            OnPropertyChanged(nameof(IsHistoricoVisible));
            OnPropertyChanged(nameof(IsSobreVisible));
            OnPropertyChanged(nameof(IsAvaliacoesVisible));
        }

        [RelayCommand]
        public void SelectTab(string tabName)
        {
            SelectedTab = tabName;
        }


        // ---------------------------------------------------------
        // CARREGAR MÚSICO
        // ---------------------------------------------------------

        public async Task CarregarMusico()
        {
            try
            {
                var musicoResultado = await _mService.GetMusicianById(IdMusico);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Musico = musicoResultado;
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar músico: {ex.Message}");
                Musico = null;
            }
        }


        // ---------------------------------------------------------
        // POPUP DE PROPOSTA
        // ---------------------------------------------------------

        [ObservableProperty]
        private bool isProposalSend = false;

        [ObservableProperty]
        private string valorProposta;
        public ICommand ProposalCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SendCommand { get; }

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
                if (EventoSelecionado == null)
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
                    p.evento = new Evento();
                    p.idRecebedor = IdMusico;

                    // ✅ CORREÇÃO AQUI: Atribua o valor double convertido
                    p.valor = valorNumerico;

                    p.idEvento = EventoSelecionado.idEvento;

                    await _pService.PostPropostaAsync(p);

                    await Application.Current.MainPage.DisplayAlert(
                        "Sucesso!",
                        "Proposta Enviada Com Sucesso para o Músico!",
                        "OK"
                    );
                    proposta = p;
                    return proposta; // Retorna a proposta em caso de sucesso
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


        [ObservableProperty]
        private Evento eventoSelecionado;
        public async Task CarregarEventosContratante(long contratanteId)
        {
            try
            {
                // Substitua esta linha pelo seu método de serviço real
                ObservableCollection<Evento> lista = await _eService.GetEventsByHirerId(contratanteId);

                EventosDisponiveis.Clear();
                foreach (var evento in lista)
                {
                    EventosDisponiveis.Add(evento);
                }

                // Opcional: Pré-selecionar o primeiro evento
                EventoSelecionado = EventosDisponiveis.FirstOrDefault();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Falha ao carregar eventos: " + ex.Message, "OK");
            }
        }
    }
}
