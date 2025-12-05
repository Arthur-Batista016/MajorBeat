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
    partial class ProposalViewModel:ObservableObject
    {

        //private readonly ProposalService _pService = new ProposalService();
        private readonly EventService _eService = new EventService();
        private string token;
        [ObservableProperty]
        public ObservableCollection<Evento> eventos_recebidos = new ObservableCollection<Evento>();

        [ObservableProperty]
        public ObservableCollection<Proposta> propostasRecebidas = new ObservableCollection<Proposta>();

        private long usuarioId;
        public ProposalViewModel()
        {
            usuarioId = Preferences.Get("Usuarioid", 0L);
            _ =GetAllNotifications(usuarioId);
            //_pService = new ProposalService(token);
            _eService = new EventService(token);
        }




        //TELA DE NOTIFICAÇÕES


        private ObservableCollection<Proposta> GetMockedProposals()
        {
            // Crie e retorne a lista de teste
            return new ObservableCollection<Proposta>
            {
                new Proposta
                {
                    Id = 1,
                    // Garanta que o modelo Contratante está preenchido para o Binding no XAML
                    contratante = new Contratante { nome = "Marquinhos" },
                    valor = "300,00", // Usando string como definido no seu modelo
                    // Adicione um campo para o tempo se ele existir no seu modelo Proposta
                    // TempoRecebido = "3h" 
                },
                new Proposta
                {
                    Id = 2,
                    contratante = new Contratante { nome = "Gisele Produções" },
                    valor = "950,00",
                    // TempoRecebido = "1d" 
                }
                // Adicione quantos objetos Proposta de teste você precisar
            };
        }

        public async Task<ObservableCollection<Proposta>> GetAllNotifications(long usuarioId)
        {
            try
            {
                // ObservableCollection<Proposta> propostas = await _pService.GetProposalByUserId(usuarioId);
                //propostasRecebidas = propostas;
                //return propostasRecebidas;



                var mockPropostas = GetMockedProposals();

                // Atribui o resultado mockado à propriedade observável
                // Isso garante que a UI seja atualizada.
                PropostasRecebidas = mockPropostas;

                // Retorna a lista mockada
                return PropostasRecebidas;

            }
            catch(Exception ex)
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
