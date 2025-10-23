using MajorBeat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Services.Usuarios
{
    public class UsuarioService : Request
    {
        private readonly Request _request;

        private const string apiUrlBase = "https://majorbeat-fzedc4ekbuaufncw.brazilsouth-01.azurewebsites.net/";

        public UsuarioService()
        {
            _request = new Request();
        }
        private string _token = string.Empty;

        public UsuarioService(string token)
        {
            _request = new Request();
            _token = token;
        }
        public async Task<Musico> PostMusicoAsync(Musico u)
        {
            string urlComplementar = "Musico/cadastrar";
            u = await _request.PostAsync(apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }

        public async Task<Contratante> PostContratanteAsync(Contratante u)
        {
            string urlComplementar = "Contratante/cadastrar";
            u = await _request.PostAsync(apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }

        public async Task<Contratante> PostAutenticarUsuarioAsync(Contratante u)
        {
            string urlComplementar = "Contratante/login";
            u = await _request.PostAsync(apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }

        public async Task<Musico> PostAutenticarUsuarioMAsync(Musico u)
        {
            string urlComplementar = "Musico/login";
            u = await _request.PostAsync(apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }

        public async Task<Evento> PostEventoAsync(Evento evento)
        {
            string urlComplementar = "Eventos/criar"; // Se a rota for algo como /api/musico ou /api/musico/cadastrar, altere aqui
            evento = await _request.PostAsync(apiUrlBase + urlComplementar, evento, _token);
            return evento;
        }

        /* public async Task<Musico> PostMusicoAsync(Musico musico)
         {
             string urlComplementar = "Musico/cadastrar"; // Se a rota for algo como /api/musico ou /api/musico/cadastrar, altere aqui
             Musico musicoCadastrado = await _request.PostAsync(apiUrlBase+urlComplementar, musico);
             return musicoCadastrado;
         }

         public async Task<Evento> PostEventoAsync(Evento evento)
         {
             string urlComplementar = "Eventos/criarEvento"; // Se a rota for algo como /api/musico ou /api/musico/cadastrar, altere aqui
             Evento eventoCadastrado = await _request.PostAsync(apiUrlBase + urlComplementar, evento);
             return eventoCadastrado;
         }

         public async Task<Contratante> PostContratanteAsync(Contratante contratante)
         {
             string urlComplementar = "Contratante/cadastrar"; // Se a rota for algo como /api/musico ou /api/musico/cadastrar, altere aqui
             Contratante contratanteCadastrado = await _request.PostAsync(apiUrlBase+urlComplementar, contratante);
             return contratanteCadastrado;
         }*/


    }
}
