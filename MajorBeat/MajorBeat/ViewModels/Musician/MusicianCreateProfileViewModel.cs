using MajorBeat.Models;
using MajorBeat.Models.Enums;
using MajorBeat.Services.Usuarios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Musician
{
    public class MusicianCreateProfileViewModel : BaseViewModel
    {

        private Musico musico { get; set; }
        public List<GeneroEnum> TodosGeneros { get; }

        public List<InstrumentoEnum> TodosInstrumentos { get; }
        public ObservableCollection<InstrumentoEnum> InstrumentosSelecionados { get; set; } = new();
        public ObservableCollection<GeneroEnum> GenerosSelecionados { get; set; } = new();

        public ICommand AddPhotoCommand { get; }
        public ImageSource FotoSelecionada { get; set; }

        public ICommand ExibirResumoCommand { get; }

        private ObservableCollection<InstrumentoEnum> _instrumentosFiltrados;
        public ObservableCollection<InstrumentoEnum> InstrumentosFiltrados
        {
            get => _instrumentosFiltrados;
            set
            {
                _instrumentosFiltrados = value;
                onPropertyChanged(nameof(InstrumentosFiltrados));
            }
        }

        private ObservableCollection<GeneroEnum> _generosFiltrados;
        public ObservableCollection<GeneroEnum> GenerosFiltrados
        {
            get => _generosFiltrados;
            set
            {
                _generosFiltrados = value;
                onPropertyChanged(nameof(GenerosFiltrados));
            }
        }

        private string _textoBusca;
        public string TextoBusca
        {
            get => _textoBusca;
            set
            {
                if (_textoBusca != value)
                {
                    _textoBusca = value;
                    onPropertyChanged(nameof(TextoBusca));
                    FiltrarGeneros();
                }
            }
        }
        private string _textoBuscaInstrumento;
        public string TextoBuscaInstrumento
        {
            get => _textoBuscaInstrumento;
            set
            {
                if (_textoBuscaInstrumento != value)
                {
                    _textoBuscaInstrumento = value;
                    onPropertyChanged(nameof(TextoBuscaInstrumento));
                    FiltrarInstrumentos();
                }
            }
        }

        public MusicianCreateProfileViewModel(Musico m)
        {
            TodosGeneros = Enum.GetValues(typeof(GeneroEnum)).Cast<GeneroEnum>().ToList();
            GenerosFiltrados = new ObservableCollection<GeneroEnum>(TodosGeneros);
            TodosInstrumentos = Enum.GetValues(typeof(InstrumentoEnum)).Cast<InstrumentoEnum>().ToList();

            // Mostra todos inicialmente
            InstrumentosFiltrados = new ObservableCollection<InstrumentoEnum>(TodosInstrumentos);
            musico = m;
            if (musico.tipoMusico == TipoMusico.SOLO)
            {

                IsVisible = true;
                cIsVisible = false;

            }
            else if (musico.tipoMusico == TipoMusico.BANDA)
            {
                IsVisible = false;
                cIsVisible = true;



            }
            ExibirResumoCommand = new Command(async () => await ExibirResumoCadastro());
            AddPhotoCommand = new Command(async () => await OnAddPhotoClicked());
        }

        private bool erroInstrumentoVisible;
        public bool ErroInstrumentoVisible
        {
            get => erroInstrumentoVisible;
            set { erroInstrumentoVisible = value; onPropertyChanged(); }
        }

        private bool erroGeneroVisible;
        public bool ErroGeneroVisible
        {
            get => erroGeneroVisible;
            set { erroGeneroVisible = value; onPropertyChanged(); }
        }

        private bool erroBioVisible;
        public bool ErroBioVisible
        {
            get => erroBioVisible;
            set { erroBioVisible = value; onPropertyChanged(); }
        }

        private bool erroUserVisible;
        public bool ErroUserVisible
        {
            get => erroUserVisible;
            set { erroUserVisible = value; onPropertyChanged(); }
        }



        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                onPropertyChanged(nameof(IsVisible));
            }
        }

        private bool c_isVisible;
        public bool cIsVisible
        {
            get => c_isVisible;
            set
            {
                c_isVisible = value;
                onPropertyChanged(nameof(cIsVisible));
            }
        }

        private string biografia = string.Empty;
        public string Biografia
        {
            get { return biografia; }
            set
            {
                biografia = value;
                onPropertyChanged();
            }
        }

        private string username = string.Empty;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                onPropertyChanged();
            }
        }

        private byte[] _fotoBytes;
        public byte[] FotoBytes
        {
            get => _fotoBytes;
            set
            {
                _fotoBytes = value;
                onPropertyChanged(nameof(FotoBytes));
            }
        }

        private string linkLinkedin;
        public string LinkLinkedin
        {
            get => linkLinkedin;
            set
            {
                linkLinkedin = value;
                onPropertyChanged(nameof(LinkLinkedin));
            }
        }

        private string linkInsta;
        public string LinkInsta
        {
            get => linkInsta;
            set
            {
                linkInsta = value;
                onPropertyChanged(nameof(LinkInsta));
            }
        }

        private string linkTwitter;
        public string LinkTwitter
        {
            get => linkTwitter;
            set
            {
                linkTwitter = value;
                onPropertyChanged(nameof(LinkTwitter));
            }
        }

        private string linkFacebook;
        public string LinkFacebook
        {
            get => linkFacebook;
            set
            {
                linkFacebook = value;
                onPropertyChanged(nameof(LinkFacebook));
            }
        }



        private async Task OnAddPhotoClicked()
        {
            await Task.Yield(); // Libera o UI thread
            await Task.Delay(100);
            string action = await Application.Current.MainPage.DisplayActionSheet(
                "Adicionar Foto", "Cancelar", null, "Escolher da Galeria", "Tirar Foto");

            FileResult photo = null;

            try
            {
                if (action == "Escolher da Galeria")
                {
                    photo = await MediaPicker.PickPhotoAsync();
                }
                else if (action == "Tirar Foto")
                {
                    photo = await MediaPicker.CapturePhotoAsync();
                }

                if (photo != null)
                {
                    using var originalStream = await photo.OpenReadAsync();

                    // Copia para memória para reutilizar
                    using var memoryStream = new MemoryStream();
                    await originalStream.CopyToAsync(memoryStream);
                    FotoBytes = memoryStream.ToArray();

                    // Cria nova cópia do stream para a imagem
                    FotoSelecionada = ImageSource.FromStream(() => new MemoryStream(FotoBytes));
                    onPropertyChanged(nameof(FotoSelecionada));
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro", $"Não foi possível obter a imagem: {ex.Message}", "OK");
            }
        }

        private void FiltrarInstrumentos()
        {
            if (string.IsNullOrWhiteSpace(TextoBuscaInstrumento))
            {
                InstrumentosFiltrados = new ObservableCollection<InstrumentoEnum>(TodosInstrumentos);
            }
            else
            {
                var filtro = TextoBuscaInstrumento.ToLowerInvariant();
                var filtrados = TodosInstrumentos
                    .Where(i => i.ToString().ToLowerInvariant().Contains(filtro))
                    .ToList();

                InstrumentosFiltrados = new ObservableCollection<InstrumentoEnum>(filtrados);
            }
        }
        private void FiltrarGeneros()
        {
            var filtro = _textoBusca?.ToLower() ?? "";

            var listaFiltrada = TodosGeneros
                .Where(g => g.ToString().ToLower().Contains(filtro))
                .ToList();

            GenerosFiltrados.Clear();
            foreach (var item in listaFiltrada)
                GenerosFiltrados.Add(item);
        }
        private bool ValidarCampos()
        {
            var valido = true;
            if (string.IsNullOrWhiteSpace(Username) && musico.tipoMusico == TipoMusico.SOLO)
            {
                ErroUserVisible = true;
                valido = false;
            }
            else
            {
                ErroUserVisible = false;
            }
            if (string.IsNullOrWhiteSpace(Biografia))
            {
                ErroBioVisible = true;
                valido = false;
            }
            else
            {
                ErroBioVisible = false;
            }

            if (InstrumentosSelecionados == null || InstrumentosSelecionados.Count == 0)
            {
                ErroInstrumentoVisible = true;
                valido = false;
            }
            else
            {
                ErroInstrumentoVisible = false;
            }

            if (GenerosSelecionados == null || GenerosSelecionados.Count == 0)
            {
                ErroGeneroVisible = true;
                valido = false;
            }
            else
            {
                ErroGeneroVisible = false;
            }


            return valido;
        }

        private async Task ExibirResumoCadastro()
        {

            if (!ValidarCampos())
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Por favor, corrija os erros nos campos destacados.", "OK");
                return; // impede de prosseguir
            }
            try
            {
                var usuario = musico;
                usuario.biografia = Biografia;
                usuario.apelido = Username;
                usuario.FotoBytes = FotoBytes;
                usuario.instrumentos = InstrumentosSelecionados.ToList();
                usuario.generos = GenerosSelecionados.ToList();
                usuario.linkInsta = LinkInsta;
                usuario.linkTwitter = LinkTwitter;
                usuario.linkFacebook = LinkFacebook;
                usuario.linkLinkdin = LinkLinkedin;


                usuario.RedesSociais = new List<string>
    {
            usuario.linkLinkdin,
            usuario.linkInsta,
            usuario.linkFacebook,
            usuario.linkTwitter,
    };/*

            string resumo =
                $"Nome: {usuario.nome}\n" +
                $"Email: {usuario.email}\n" +
                $"Biografia: {usuario.biografia}\n" +
                $"Telefone: {usuario.telefone}\n" +
                $"Logradouro: {usuario.logradouro}, Nº {usuario.numero}\n" +
                $"Bairro: {usuario.bairro}\n" +
                $"Cidade: {usuario.cidade} - {usuario.uf}\n" +
                $"CEP: {usuario.cep}\n" +
                $"Senha: {usuario.senha}\n" +
                $"NomePerfil: {usuario.username}\n"+
                $"TipoMusico: {usuario.tipoMusico}\n"+
                $"Bytes:{usuario.FotoBytes}\n"+
                $"Instrumentos: {string.Join(", ", usuario.instrumentos)}\n" +
                $"Gêneros: {string.Join(", ", usuario.generos)}\n" +
                $"Links: {string.Join(", ", usuario.RedesSociais)}";


            await Application.Current.MainPage.DisplayAlert("Resumo do Cadastro", resumo, "OK");*/

                var service = new UsuarioService();
                var musicoCadastrado = await service.PostMusicoAsync(usuario);
                usuario.id = musicoCadastrado.id;
                // Exibe mensagem de sucesso com o ID retornado
                await Application.Current.MainPage.DisplayAlert(
                    "Sucesso",
                    $"Músico {musicoCadastrado.nome} cadastrado com sucesso!\nID: {usuario.id}",
                    "OK"
                );

                // Retorna à página anterior (ou navega conforme sua lógica)
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro ao cadastrar",
                    $"Não foi possível concluir o cadastro.\nDetalhes: {ex.Message}",
                    "OK"
                );
            }
        }
    }
}
