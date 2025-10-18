using MajorBeat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Services.Usuarios
{
    public class UsuarioService:Request
    {
        private readonly Request _request;

        private const string apiUrlBase = "http://localhost:8080/";

        public UsuarioService()
        {
            _request = new Request();
        }

        public async Task<Musico> PostMusicoAsync(Musico musico)
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
        }


    }
}
