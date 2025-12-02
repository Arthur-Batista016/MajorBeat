using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Models;
using MajorBeat.Services.Musicians;
using MajorBeat.Services.Users;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Users
{
    public partial class DetailsViewModel : ObservableObject
    {
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


        // ---------------------------------------------------------
        // CONSTRUTOR
        // ---------------------------------------------------------
        public DetailsViewModel(long id)
        {
            IdMusico = id;
            _ = CarregarMusico();

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

        public async Task SendProposal()
        {
            try
            {
                IsProposalSend = false;
                MessageVisibility = true;

                await Application.Current.MainPage.DisplayAlert(
                    "Sucesso!",
                    "Proposta Enviada Com Sucesso para o Músico!",
                    "OK"
                );
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
