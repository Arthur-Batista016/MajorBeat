using CommunityToolkit.Mvvm.ComponentModel;
using MajorBeat.Models;
using MajorBeat.Services.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.ViewModels.Users
{
    public class ProposalViewModel:ObservableObject
    {
        private readonly EventService _eService = new EventService();
        private string token;

        public ObservableCollection<Evento> eventos_recebidos;


        public ProposalViewModel()
        {

            _eService = new EventService(token);
        }




        //TELA DE NOTIFICAÇÕES

        public async Task<ObservableCollection<Evento>> GetAllNotifications(long musician_id)
        {
            try
            {
                ObservableCollection<Evento> eventos = await _eService.GetEventsByHirerId(musician_id);
                eventos_recebidos = eventos;
                return eventos_recebidos;

            
            }catch(Exception ex)
            {
                return null;
            }
        }








        //TELA DE PROPOSTA


        public async Task<Evento> EventProposal(long id_evento)
        {
            try
            {
                Evento evento_proposta = await _eService.GetEventById(id_evento);
                return evento_proposta;
            }
            catch (Exception ex) {

                return null;
            }

        }



       

        public async Task RefuseProposal()
        {

        }

        public async Task AcceptProposal()
        {

            try
            {

            }
            catch (Exception ex) { 
            
                
            }
        }




       



    }
}
