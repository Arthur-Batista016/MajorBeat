using CommunityToolkit.Mvvm.ComponentModel;
using MajorBeat.Models;
using MajorBeat.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.ViewModels.Users
{
    public partial class ProposalDetailsViewModel:ObservableObject
    {

        private PropostaService _pService;

        [ObservableProperty]
        private long propostaId;

        private Proposta propostaInfos;


        public ProposalDetailsViewModel(long id_proposta)
        {
            propostaId = id_proposta;
            /*_ =  GetProposalInfos(propostaId);*/

        }


        /*public async Task<Proposta> GetProposalInfos(long id) 
        {
            try
            {
                Proposta proposta = await _pService.GetProposalByIdContratante(id);
                propostaInfos = proposta;
                return propostaInfos;

            }
            catch (Exception ex) {

                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
                return null;
            }
        }*/

        public async Task<Proposta> AcceptProposal()
        {
            try
            {
                propostaInfos.statusProposta = Enums.StatusProposta.ACEITO;
                return propostaInfos;

            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
                return null;
            }
        }

        public async Task<Proposta> RefuseProposal()
        {
            try
            {
                propostaInfos.statusProposta = Enums.StatusProposta.RECUSADO;
                return propostaInfos;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
                return null;
            }
        }


    }
}
