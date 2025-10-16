using MajorBeat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Services.Musician
{
    public class ProfilePageService : Request
    {
        private readonly Request _request;
        private const string _baseUrl = "localhost:8080/Musico";

        public async Task<ObservableCollection<Musico>> getAllMusicians()
        {
            string urlComplementar = "/getAllMusicos";
            ObservableCollection<Musico> musicos = await _request.GetAsync<ObservableCollection<Models.Musico>>(_baseUrl + urlComplementar);
            return musicos;
        }
    }
}
