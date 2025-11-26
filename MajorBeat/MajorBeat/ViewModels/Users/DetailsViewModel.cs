using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Models;
using MajorBeat.Services.Musicians;
using MajorBeat.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.ViewModels.Users
{
    public partial class DetailsViewModel:ObservableObject
    {
        MusicianService _mService = new MusicianService();
        EventService _eService = new EventService();

        [ObservableProperty]
        private long idMusico;

        [ObservableProperty]
        private Musico musico;


        public DetailsViewModel(long id)
        {
            IdMusico = id;
            _ = CarregarMusico();
        }


        [ObservableProperty] private string selectedTab = "Historico";

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
        public async Task CarregarMusico()
        {
            try
            {
                var musicoResultado = await _mService.GetMusicianById(IdMusico);

                // 2. FORCE a atualização a acontecer na Thread Principal
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Musico = musicoResultado;
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar músico: {ex.Message}");
                // Você pode querer setar como nulo em caso de erro
                musico = null;
            }
        }





    }
}
