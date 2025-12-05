using MajorBeat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Services.Users
{
    public class PropostaService
    {
        private readonly Request _request;
        private const string _baseUrl = "https://majorbeat-fzedc4ekbuaufncw.brazilsouth-01.azurewebsites.net/Propostas";
        private string _token;
        public PropostaService()
        {
            _request = new Request();
            _token = Preferences.Get("UsuarioToken", string.Empty);
        }
        public PropostaService(string token)
        {
            _request = new Request();
            _token = token;
        }

        // Permite atualizar o token a qualquer momento
        public void SetToken(string token)
        {
            _token = token;
            Preferences.Set("UsuarioToken", token);
        }




        public string Token => _token;
        public async Task<ObservableCollection<Proposta>> GetProposalByIdContratante(long id)
        {
            string urlComplementar = "/GetProposalByIdContratante";
            ObservableCollection<Proposta> propostas = await
            _request.GetAsync<ObservableCollection<Proposta>>(_baseUrl + urlComplementar, _token);
            return propostas;
        }

        public async Task<ObservableCollection<Proposta>> GetProposalByMusico(long id)
        {
            string urlComplementar = "/GetProposalByMusico";
            ObservableCollection<Proposta> propostas = await
            _request.GetAsync<ObservableCollection<Proposta>>(_baseUrl + urlComplementar, _token);
            return propostas;
        }

        public async Task<Proposta> GetProposalById(long id)
        {
            string urlComplementar = $"/GetById/{id}";
            Proposta proposta = await
            _request.GetAsync<Proposta>(_baseUrl + urlComplementar, _token);
            return proposta;
        }

    }
}
