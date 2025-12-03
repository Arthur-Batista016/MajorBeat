using MajorBeat.Enums;
using MajorBeat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Services.Users
{
    public class EventService:Request
    {
        private readonly Request _request;
        private const string _baseUrl = "https://majorbeat-fzedc4ekbuaufncw.brazilsouth-01.azurewebsites.net/Eventos";
        private string _token;

        public EventService()
        {
            _request = new Request();
            _token = Preferences.Get("UsuarioToken", string.Empty);
        }

        // Construtor opcional para passar token manualmente
        public EventService(string token)
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

        public async Task<ObservableCollection<Evento>> GetAllEvents()
        {
            string urlComplementar = "/getAll";
            ObservableCollection<Evento> eventos = await
            _request.GetAsync<ObservableCollection<Evento>>(_baseUrl + urlComplementar, _token );
            return eventos;
        }

        public async Task<Evento> GetEventById(long id)
        {
                string urlComplementar = $"/getById/{id}";
                Evento evento = await _request.GetAsync<Evento>(_baseUrl + urlComplementar, _token);
                return evento;  
         }

        public async Task<ObservableCollection<Evento>> GetEventsByGenre(NomeGenero nomeGenero)
        {
            string urlComplementar = $"/getByGenero/{nomeGenero}";
            ObservableCollection<Evento> evento = await _request.GetAsync<ObservableCollection<Evento>>(_baseUrl + urlComplementar, _token);
            return evento;
        }

        public async Task<ObservableCollection<Evento>> GetEventsByHirerId(long hirer_id)
        {
            string urlComplementar = $"/getByIdContratante/{hirer_id}";
            ObservableCollection<Evento> evento = await _request.GetAsync<ObservableCollection<Evento>>(_baseUrl + urlComplementar, _token);
            return evento;
        }

      


        public async Task<ObservableCollection<Evento>> GetEventByTipoEvento(TipoEvento tipoEvento)
        {
            string urlComplementar = $"/getByTipoEvento/{tipoEvento}";
            ObservableCollection<Evento> evento = await _request.GetAsync<ObservableCollection<Evento>>(_baseUrl + urlComplementar, _token);
            return evento;
        }

        public Evento GetEventByData(DateTime tipoMusico)
        {
            return null;
        }

        public Evento GetEventByEndereco(string endereco)
        {
            return null;
        }

        public Evento GetEventByNome(string nome)
        {
            return null;
        }

        public Evento PostEvent()
        {
            return null;
        }

        public void DeleteEvent() 
        { 
        
        }
    }
}
