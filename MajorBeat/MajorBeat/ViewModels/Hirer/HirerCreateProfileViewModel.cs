using MajorBeat.ModelsJeff;
using MajorBeat.Services;
using MajorBeat.Services.Usuarios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MajorBeat.ViewModels.Hirers
{
    public class HirerCreateProfileViewModel : BaseViewModel
    {
        private readonly MediaService _mediaService;
        public ObservableCollection<FileResult> ArquivosDeMediaSelecionados { get; set; }
        public ICommand SelecionarMediaCommand { get; }
        public ICommand ExibirResumoCommand { get; set; }
        private readonly UsuarioService uService;
        public ICommand AddPhotoCommand { get; }
        public ImageSource FotoSelecionada { get; set; }

        public Contratante Usuario { get; private set; }
        private async Task SelecionarMedia()
        {
            try
            {
                // Permite selecionar vários
                var results = await FilePicker.Default.PickMultipleAsync(new PickOptions
                {
                    PickerTitle = "Selecione imagens ou vídeos",
                    // Deixe em branco para pegar qualquer tipo, ou especifique
                    // FileTypes = FilePickerFileType.Images 
                });

                if (results != null)
                {
                    foreach (var file in results)
                    {
                        ArquivosDeMediaSelecionados.Add(file);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Falha ao selecionar mídia: {ex.Message}", "OK");
            }
        }
        public HirerCreateProfileViewModel(Contratante cc)
        {
            _mediaService = new MediaService();
            ArquivosDeMediaSelecionados = new ObservableCollection<FileResult>();
            SelecionarMediaCommand = new Command(async () => await SelecionarMedia());

            Usuario = cc;
            AddPhotoCommand = new Command(async () => await OnAddPhotoClicked());
            ExibirResumoCommand = new Command(async () => await ExibirResumoCadastro());

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

        private string nomePerfil = string.Empty;
        public string NomePerfil
        {
            get { return nomePerfil; }
            set
            {
                nomePerfil = value;
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

        private bool ValidarCampos()
        {
            var valido = true;
            if (string.IsNullOrWhiteSpace(NomePerfil))
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


            return valido;
        }
        private async Task ExibirResumoCadastro()
        {
            
            if (!ValidarCampos())
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Por favor, corrija os erros nos campos destacados.", "OK");
                return; // impede de prosseguir
            }
            try { 
            var usuario = Usuario;
            usuario.biografia = Biografia;
            usuario.nomePerfil = NomePerfil;
            usuario.FotoBytes = FotoBytes;
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
    };



            var service = new UsuarioService();
            var contratanteCadastrado = await service.PostContratanteAsync(usuario);


                
                // Exibe mensagem de sucesso com o ID retornado
                await Application.Current.MainPage.DisplayAlert(
                "Sucesso",
                $"Contratante {contratanteCadastrado.nome} cadastrado com sucesso!\nID: {contratanteCadastrado.idContratante}",
                "OK"
            );

                // Retorna à página anterior (ou navega conforme sua lógica)
                await Application.Current.MainPage.Navigation.PushAsync(new Views.Users.InitialPage());
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
