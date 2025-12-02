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

        [ObservableProperty]
        public List<string> mediaUrl;

        // === Galeria ===
         public ObservableCollection<string> Images { get; } = new()
         {
             "panelaodonorte1.png",
             "panelaodonorte2.png",
             "panelaodonorte3.png"
         };
        /*
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
        }*/

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
        public async Task LoadHirerDataAsync()
        {

            try
            {



                contratante = await uService.GetHirerById(usuarioId);

                // **ATRIBUIÇÃO DOS DADOS AQUI APÓS A ESPERA (AWAIT)**
               
                if (contratante != null)
                {


                    //MediaUrl = contratante.mediaUrl;
                    Username = contratante.nomePerfil;
                    Nome = contratante.nome;
                    Empresanome = contratante.empresa;
                    Biografia = contratante.biografia;
                    Email = contratante.email;
                    Logradouro = contratante.endereco;
                    Telefone = contratante.telefone;
                    RedesSociais = contratante.links;
                    LinkFacebook = RedesSociais[0];
                    LinkLinkdin = RedesSociais[1];
                    LinkInsta = RedesSociais[2];
                    LinkTwitter = RedesSociais[3];
                    // ... atribua todas as outras propriedades ...

                    // Exemplo de dados mockados que você estava usando para inicialização:
                    TipoContratante = TipoContratante.ESTABELECIMENTO;
                    
                }
            }
            catch (Exception ex)
            {
                // Tratar o erro de forma apropriada, talvez exibindo um alerta.
                Console.WriteLine($"Erro ao carregar dados do contratante: {ex.Message}");
            }
        }
        public long usuarioId;
        public Contratante contratante;
        public ProfileHirerViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            uService = new HirerService(token);
            usuarioId = Preferences.Get("Usuarioid", 0L);
            

    }
    }
}