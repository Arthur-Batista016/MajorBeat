using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Models.Enums;
using System.Collections.ObjectModel;

namespace MajorBeat.ViewModels.Musicians
{
    public partial class ProfileMusicianViewModel : ObservableObject
    {
        [ObservableProperty]
        public int id;

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
        public TipoMusico tipoMusico;

        [ObservableProperty]
        public List<NomeInstrumento> instrumentos;

        [ObservableProperty]
        public List<NomeGenero> generos;

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
            "musicianprofile1.png",
            "musicianprofile2.png",
            "musicianprofile3.png"
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
        [ObservableProperty] private string selectedTab = "Galeria";

        public bool IsGaleriaVisible => SelectedTab == "Galeria";
        public bool IsSobreVisible => SelectedTab == "Sobre";
        public bool IsAvaliacoesVisible => SelectedTab == "Avaliacoes";

        partial void OnSelectedTabChanged(string value)
        {
            OnPropertyChanged(nameof(IsGaleriaVisible));
            OnPropertyChanged(nameof(IsSobreVisible));
            OnPropertyChanged(nameof(IsAvaliacoesVisible));
        }

        [RelayCommand]
        public void SelectTab(string tabName)
        {
            SelectedTab = tabName;
        }

        // === Construtor ===
        public ProfileMusicianViewModel()
        {
            Username = "Marquinhos";
            Nome = "Marcos José";
            Biografia = "Oi, eu sou o Marquinhos. Minha música é um pedaço de mim, uma mistura das minhas raízes e das minhas descobertas pelo caminho.";
            Instrumentos = new List<NomeInstrumento> { NomeInstrumento.GUITARRA };
            Generos = new List<NomeGenero> { NomeGenero.SERTANEJO };
            Email = "marcos.jose123@gmail.com";
            Logradouro = "R. Alcântara, 113 - Vila Guilherme,\nSão Paulo - SP, 02110-010";
            Telefone = "(11) 99999-9999";
            LinkFacebook = "marcos.jose";
            LinkLinkdin = "marcos.jose";
            LinkInsta = "marcos.jose";
            LinkTwitter = "marcos.jose";
        }
    }
}
