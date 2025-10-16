using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Propriedades da galeria        
        
        public ObservableCollection<string> Images { get; } = new()
        {
            "musicianprofile1.png",
            "musicianprofile2.png",
            "musicianprofile3.png"
        };

        [ObservableProperty]
        public int currentIndex;

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
    }
}
