using MajorBeat.Enums;
using MajorBeat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Services.Musicians
{
    public class MusicianService
    {
        private readonly Request _request;
        private const string _baseUrl = "http://localhost:8080/Musico";
        private string _token;
        public string Token => _token;



        public MusicianService()
        {
            _request = new Request();
            _token = Preferences.Get("UsuarioToken", string.Empty);

        }


        public MusicianService(string token)
        {
            _request = new Request();
            _token = token;
        }

       
        public void SetToken(string token)
        {
            _token = token;
            Preferences.Set("UsuarioToken", token); // opcional: atualiza Preferences também
        }



        public async Task<ObservableCollection<Musico>> GetAllMusicians()
        {
            string urlComplementar = "/getAllMusicos";
            ObservableCollection<Musico> musicos = await
            _request.GetAsync<ObservableCollection<Musico>>(_baseUrl + urlComplementar, _token);
            return musicos;
        }

        public async Task<ObservableCollection<Musico>> GetMusicianByName(string nome)
        {
            string urlComplementar = $"/getByGenero/{nome}";
            ObservableCollection<Musico> musicos = await _request.GetAsync<ObservableCollection<Musico>>(_baseUrl + urlComplementar, _token);
            return musicos;
        }

        public async Task<ObservableCollection<Musico>> GetMusicianByType(TipoMusico tipoMusico)
        {
            string urlComplementar = $"/getByGenero/{tipoMusico}";
            ObservableCollection<Musico> musicos = await _request.GetAsync<ObservableCollection<Musico>>(_baseUrl + urlComplementar, _token);
            return musicos;
        }


        public async Task<ObservableCollection<Musico>> GetMusicianByGenre(NomeGenero nomeGenero)
        {
            string urlComplementar = $"/getByGenero/{nomeGenero}";
            ObservableCollection<Musico> musicos = await _request.GetAsync<ObservableCollection<Musico>>(_baseUrl + urlComplementar, _token);
            return musicos;
        }
    }
}
