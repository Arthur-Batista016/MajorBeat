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


        public DetailsViewModel(long id)
        {
            IdMusico = id;
            _ = CarregarMusico();
        }



        public async Task<Musico> CarregarMusico()
        {
            try
            {
                Musico musico = await _mService.GetMusicianById(IdMusico);
                return musico;
            }
            catch (Exception ex) {

                System.Diagnostics.Debug.WriteLine($"Erro ao carregar músico: {ex.Message}");
                return null;
            }

        }





    }
}
