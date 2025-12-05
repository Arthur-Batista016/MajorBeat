using CommunityToolkit.Mvvm.ComponentModel;
using MajorBeat.Enums;
using MajorBeat.Models;
using MajorBeat.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Users
{
    public partial class ProposalDetailsViewModel:ObservableObject
    {

        private PropostaService _pService;

        [ObservableProperty]
        private long propostaId;




        [ObservableProperty]
        private string nomeRemetente;


        [ObservableProperty]
        private string nomeRecebedor;


        [ObservableProperty]
        private Proposta propostaInfos;
        private string token;
        private string role;
        public ICommand aceitarCommand { get; set; }
        public ICommand recusarCommand { get; set; }
        public ProposalDetailsViewModel(long id_proposta)
        {
            recusarCommand = new Command(async () => await RefuseProposal());
            aceitarCommand = new Command(async () => await AcceptProposal());
            token = Preferences.Get("UsuarioToken", string.Empty);
            role = Preferences.Get("role", string.Empty);
            _pService = new PropostaService(token);
            propostaId = id_proposta;
            _ =  GetProposalInfos(propostaId);

        }


        public async Task<Proposta> GetProposalInfos(long id) 
        {
            try
            {
                Proposta proposta = await _pService.GetProposalById(id);
                if (role == "musico")
                {
                    NomeRemetente = proposta.contratante.nome;
                    NomeRecebedor = proposta.musico.nome;
                }
                else
                {
                    NomeRecebedor = proposta.contratante.nome;
                    NomeRemetente = proposta.musico.nome;
                }
                PropostaInfos = proposta;
                return PropostaInfos;

            }
            catch (Exception ex) {

                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
                return null;
            }
        }

        public async Task<Proposta> AcceptProposal()
        {
            try
            {
                propostaInfos.statusProposta = Enums.StatusProposta.ACEITO;
                //await _pService.UpdateProposta(propostaInfos);
                await Application.Current.MainPage.DisplayAlert("Aviso", "Proposta aceita com sucesso!!", "ok");


                await Application.Current.MainPage.Navigation.PushAsync(new Views.Hirers.HirerHomePage());
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
                //await _pService.UpdateProposta(propostaInfos);
                await Application.Current.MainPage.DisplayAlert("Aviso", "Proposta recusada com sucesso", "ok");


                await Application.Current.MainPage.Navigation.PushAsync(new Views.Hirers.HirerHomePage());
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
