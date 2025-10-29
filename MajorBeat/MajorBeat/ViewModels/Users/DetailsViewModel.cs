using CommunityToolkit.Mvvm.ComponentModel;
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
