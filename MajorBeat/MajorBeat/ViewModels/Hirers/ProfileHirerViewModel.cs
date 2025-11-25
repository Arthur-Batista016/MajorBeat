using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Enums;
using MajorBeat.Models;
using MajorBeat.Services.Hirers;
using MajorBeat.Services.Usuarios;
using System.Collections.ObjectModel;

namespace MajorBeat.ViewModels.Hirers
{
    public partial class ProfileHirerViewModel : ObservableObject
    {
        [ObservableProperty]
        public long id;

        [ObservableProperty]
        public string nome;

        [ObservableProperty]
        public string email;

        [ObservableProperty]
        public string telefone;

        [ObservableProperty]
        public string logradouro;

        [ObservableProperty]
        public string numero;

        [ObservableProperty]
        public string cep;

        [ObservableProperty]
        public string bairro;

        [ObservableProperty]
        public string cidade;

        [ObservableProperty]
        public string uf;

        [ObservableProperty]
        public string senha;

        [ObservableProperty]
        public string biografia;

        [ObservableProperty]
        public string username;

        [ObservableProperty]
        public TipoContratante tipoContratante;

        [ObservableProperty]
        public string empresanome;

        [ObservableProperty]
        public byte[] fotoBytes;

        [ObservableProperty]
        public string linkLinkdin;

        [ObservableProperty]
        public string linkInsta;

        [ObservableProperty]
        public string linkTwitter;

        [ObservableProperty]
        public string linkFacebook;

        [ObservableProperty]
        public List<string> redesSociais;

        // === Galeria ===
        public ObservableCollection<string> Images { get; } = new()
        {
            "panelaodonorte1.png",
            "panelaodonorte2.png",
            "panelaodonorte3.png"
        };

        [ObservableProperty] private int currentIndex;

        public string PositionText => $"{CurrentIndex + 1}/{Images.Count}";

        partial void OnCurrentIndexChanged(int value)
        {
            OnPropertyChanged(nameof(PositionText));
        }

        [RelayCommand]
        public void Next()
        {
            if (CurrentIndex < Images.Count - 1)
                CurrentIndex++;
        }

        [RelayCommand]
        public void Previous()
        {
            if (CurrentIndex > 0)
                CurrentIndex--;
        }

        // === Tabs ===
        [ObservableProperty] private string selectedTab = "Historico";

        public bool IsHistoricoVisible => SelectedTab == "Historico";
        public bool IsSobreVisible => SelectedTab == "Sobre";
        public bool IsAvaliacoesVisible => SelectedTab == "Avaliacoes";

        partial void OnSelectedTabChanged(string value)
        {
            OnPropertyChanged(nameof(IsHistoricoVisible));
            OnPropertyChanged(nameof(IsSobreVisible));
            OnPropertyChanged(nameof(IsAvaliacoesVisible));
        }

        [RelayCommand]
        public void SelectTab(string tabName)
        {
            SelectedTab = tabName;
        }

        // === Construtor ===
        private HirerService uService;
        [RelayCommand]
        public async Task<Contratante> getporra()
        {
            try
            {
                contratante = await uService.GetHirerById(usuarioId);
                return contratante;

            }
            catch (Exception ex)
            {
                return new Contratante();
            }
        }
        public long usuarioId;
        Contratante contratante;
        public ProfileHirerViewModel()
        {
            uService=new HirerService();
            usuarioId = Preferences.Get("Usuarioid",0L);
            _ = getporra();

            Username = contratante.nomePerfil;
            Nome = contratante.nome;
            Empresanome = contratante.empresa;
            Biografia = contratante.biografia;
            TipoContratante = TipoContratante.ESTABELECIMENTO;
            Email = "jose.mota123@gmail.com";
            Logradouro = "R. Namaxi, 155 - Penha de França,\nSão Paulo - SP, 03609-020";
            Telefone = "(11) 2647-7805";
            LinkFacebook = "jose.mota";
            LinkLinkdin = "jose.mota";
            LinkInsta = "jose.mota";
            LinkTwitter = "jose.mota";
        }
    }
}
