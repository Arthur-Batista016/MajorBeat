using CommunityToolkit.Mvvm.ComponentModel;
using MajorBeat.Models;
using MajorBeat.Services.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Users
{
    partial class ProposalViewModel : ObservableObject
    {

        private readonly PropostaService _pService = new PropostaService();
        private readonly EventService _eService = new EventService();

        public string TituloNotificacao
        {
            get
            {
                // Verifica se a coleção tem itens antes de tentar acessar o índice [0]
                if (PropostasRecebidas != null && PropostasRecebidas.Count > 0)
                {
                    // Acessa o primeiro elemento da coleção
                    var primeiraProposta = PropostasRecebidas.FirstOrDefault();

                    if (primeiraProposta?.contratante != null && primeiraProposta.evento != null)
                    {
                        // Retorna a string formatada usando a primeira proposta
                        return $"{primeiraProposta.contratante.nome} enviou uma proposta para {primeiraProposta.evento.nome}!";
                    }
                }
                return "Notificação de Proposta"; // Valor de fallback
            }
        }

        private string token;
        [ObservableProperty]
        public ObservableCollection<Evento> eventos_recebidos = new ObservableCollection<Evento>();

        [ObservableProperty]
        public ObservableCollection<Proposta> propostasRecebidas = new ObservableCollection<Proposta>();

        private long usuarioId;

        private string role;
        public ProposalViewModel()
        {
            role = Preferences.Get("role", string.Empty);
            usuarioId = Preferences.Get("Usuarioid", 0L);
            _ = GetAllNotifications(usuarioId);
            _pService = new PropostaService(token);
            _eService = new EventService(token);




            ClickProposalCommand = new Command<Proposta>(async (proposta) => await ProposalTapped(proposta));
        }

        public ICommand ClickProposalCommand;


        //TELA DE NOTIFICAÇÕES




        public async Task<ObservableCollection<Proposta>> GetAllNotifications(long usuarioId)
        {
            try
            {
                ObservableCollection<Proposta> propostas;
                if (role == "musico")
                {
                    // 1. Obtém todas as propostas
                    propostas = await _pService.GetProposalByMusico(usuarioId);
                }
                else if (role == "contratante")
                {
                    propostas = await _pService.GetProposalByIdContratante(usuarioId);
                }
                else
                {
                    return new ObservableCollection<Proposta>();
                }
                if (propostas == null)
                {
                    return new ObservableCollection<Proposta>();
                }

                var propostasDoRecebedor = propostas
            .Where(p => p.idRecebedor == usuarioId); // Filtra propostas onde o ID do recebedor é o ID do usuário logado

                // FILTRO EXISTENTE: Agora aplica o filtro de status na lista já filtrada por ID
                var propostasAbertas = propostasDoRecebedor
                    .Where(p => p.statusProposta == Enums.StatusProposta.ABERTO)
                    .ToList(); // Converte para lista

                ObservableCollection<Proposta> propostasFiltradas = new ObservableCollection<Proposta>(propostasAbertas);
                PropostasRecebidas = propostasFiltradas;
                return PropostasRecebidas;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task ProposalTapped(Proposta proposta)
        {

            if (proposta == null)
                return;

            long id = proposta.idProposta;




        }






        //TELA DE PROPOSTA





        //public async Task<Evento> EventProposal(long id_proposta)
        //{
        //  try
        //  {
        //Proposta proposta = await _pService.GetProposalByIdContratante(id_proposta);
        //return proposta;
        //}
        //catch (Exception ex) {

        //  return null;
        //}

        //}





        public async Task RefuseProposal()
        {

        }

        public async Task AcceptProposal()
        {

            try
            {

            }
            catch (Exception ex)
            {


            }
        }








    }
}
