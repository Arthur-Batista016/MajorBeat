using MajorBeat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Services.Hirers
{
    public class HirerProfilePageService : Request
    {
        private readonly Request _request;
        private const string _baseUrl = "https://majorbeat-fzedc4ekbuaufncw.brazilsouth-01.azurewebsites.net/Musico";
        private string _token;
        public string Token => _token;

        public async Task<Contratante> GetContratanteByIdAsync(int id)
        {
            string urlComplementar = $"/getContratante/{id}";
            Contratante contratante = await _request.GetAsync<Models.Contratante>(_baseUrl + urlComplementar);
            return contratante;
        }
    }
}
