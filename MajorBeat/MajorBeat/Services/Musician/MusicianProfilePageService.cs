using MajorBeat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Services.Musician
{
    public class MusicianProfilePageService : Request
    {
        private readonly Request _request;
        private const string _baseUrl = "https://majorbeat-fzedc4ekbuaufncw.brazilsouth-01.azurewebsites.net/Musico";
        private string _token;
        public string Token => _token;

        public async Task<Musico> GetMusicianByIdAsync(int id)
        {
            string urlComplementar = $"/getMusico/{id}";
            Musico musico = await _request.GetAsync<Models.Musico>(_baseUrl + urlComplementar);
            return musico;
        }
    }
}
