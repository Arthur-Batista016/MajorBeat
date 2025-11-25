using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MajorBeat.Models;

namespace MajorBeat.Services.Hirers
{
    public class HirerService:Request
    {
        private readonly Request _request;

        private const string apiUrlBase = "https://majorbeat-fzedc4ekbuaufncw.brazilsouth-01.azurewebsites.net/Contratante";
        public HirerService()
        {
            _request = new Request();
        }
        private string _token = string.Empty;

        public HirerService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<Contratante> GetHirerById(long id)
        {
                string urlComplementar = $"/{id}";
                Contratante contratante = await _request.GetAsync<Contratante>(apiUrlBase + urlComplementar, _token);
                return contratante;  
         }
}
}
