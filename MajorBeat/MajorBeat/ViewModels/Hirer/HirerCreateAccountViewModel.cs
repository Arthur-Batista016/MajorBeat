using MajorBeat.Models;
using MajorBeat.Services.Usuarios;
using MajorBeat.ViewModels.Hirers;
using MajorBeat.Views.Hirers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels
{
    public class HirerCreateAccountViewModel : BaseViewModel
    {


        private UsuarioService uService;


        //  public ICommand AddPhotoCommand { get; set; }

        public ICommand ProximoCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        private readonly INavigation _navigation;

        /*  private async Task OnAddPhotoClicked()
          {
              string action = await _page.DisplayActionSheet("Adicionar Foto", "Cancelar", null, "Escolher da Galeria", "Tirar Foto");

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
                      var stream = await photo.OpenReadAsync();
                      var selectedImage = _page.FindByName<Image>("SelectedImage1");
                      selectedImage.Source = ImageSource.FromStream(() => stream);
                  }
              }
              catch (Exception ex)
              {
                  await _page.DisplayAlert("Erro", "Não foi possível obter a imagem: " + ex.Message, "OK");
              }
          }*/

        public HirerCreateAccountViewModel(INavigation navigation)
        {
            _navigation = navigation;

            //  uService = new UsuarioService();
            InicializarCommands();
            //AddPhotoCommand = new Command(async () => await OnAddPhotoClicked());

        }
        public void InicializarCommands()
        {
            ProximoCommand = new Command(async () => await UserSave());

            // RegistrarCommand = new Command(async () => await RegistrarUsuario());

        }
        public async Task UserSave()
        {
            Contratante u = new Contratante();
            u.nome = Nome;
            u.email = Email;
            u.telefone = Telefone;
            u.endereco = $"{Logradouro}, {Numero}, {Bairro}, {Cidade}, {Uf}, {Cep}";
            u.senha = Senha;
            u.empresa = Empresa;
            if (ValidarCampos(u))
            {
                var viewmodel = new HirerCreateProfileViewModel(u);
                await _navigation.PushAsync(new HirerCreateProfileView(viewmodel));
            }

        }


        private bool ValidarCampos(Contratante u)
        {
            bool valido = true;

            if (string.IsNullOrWhiteSpace(Nome))
            {
                ErroNomeVisible = true;
                valido = false;
            }
            else
            {
                ErroNomeVisible = false;
            }
            if (string.IsNullOrWhiteSpace(Email) || !Email.EndsWith(".com"))
            {
                ErroEmailVisible = true;
                valido = false;
            }
            else
            {
                ErroEmailVisible = false;
            }
            if (string.IsNullOrWhiteSpace(Telefone) || Telefone.Length != 11 || !Telefone.All(char.IsDigit))
            {
                ErroTelefoneVisible = true;
                valido = false;
            }
            else
            {
                ErroTelefoneVisible = false;
            }
            if (string.IsNullOrWhiteSpace(Logradouro))
            {
                ErroLogradouroVisible = true;
                valido = false;
            }
            else
            {
                ErroLogradouroVisible = false;
            }
            if (string.IsNullOrWhiteSpace(Numero) || !Numero.All(char.IsDigit))
            {
                ErroNumeroVisible = true;
                valido = false;
            }
            else
            {
                ErroNumeroVisible = false;
            }
            if (string.IsNullOrWhiteSpace(Bairro))
            {
                ErroBairroVisible = true;
                valido = false;
            }
            else
            {
                ErroBairroVisible = false;
            }
            if (string.IsNullOrWhiteSpace(Cidade))
            {
                ErroCidadeVisible = true;
                valido = false;
            }
            else
            {
                ErroCidadeVisible = false;
            }
            if (string.IsNullOrWhiteSpace(Uf) || Uf.Length != 2)
            {
                ErroUfVisible = true;
                valido = false;
            }
            else
            {
                ErroUfVisible = false;
            }
            if (string.IsNullOrWhiteSpace(Cep) || Cep.Length != 8 || !Cep.All(char.IsDigit))
            {
                ErroCepVisible = true;
                valido = false;
            }
            else
            {
                ErroCepVisible = false;
            }
            if (string.IsNullOrWhiteSpace(Senha) || Senha.Length < 8)
            {
                ErroSenhaVisible = true;
                valido = false;
            }
            else
            {
                ErroSenhaVisible = false;
            }


            return valido;
        }

        private bool erroNomeVisible;
        public bool ErroNomeVisible
        {
            get => erroNomeVisible;
            set { erroNomeVisible = value; onPropertyChanged(); }
        }

        private bool erroEmailVisible;
        public bool ErroEmailVisible
        {
            get => erroEmailVisible;
            set { erroEmailVisible = value; onPropertyChanged(); }
        }

        private bool erroTelefoneVisible;
        public bool ErroTelefoneVisible
        {
            get => erroTelefoneVisible;
            set { erroTelefoneVisible = value; onPropertyChanged(); }
        }

        private bool erroLogradouroVisible;
        public bool ErroLogradouroVisible
        {
            get => erroLogradouroVisible;
            set { erroLogradouroVisible = value; onPropertyChanged(); }
        }

        private bool erroNumeroVisible;
        public bool ErroNumeroVisible
        {
            get => erroNumeroVisible;
            set { erroNumeroVisible = value; onPropertyChanged(); }
        }

        private bool erroBairroVisible;
        public bool ErroBairroVisible
        {
            get => erroBairroVisible;
            set { erroBairroVisible = value; onPropertyChanged(); }
        }

        private bool erroCidadeVisible;
        public bool ErroCidadeVisible
        {
            get => erroCidadeVisible;
            set { erroCidadeVisible = value; onPropertyChanged(); }
        }

        private bool erroUfVisible;
        public bool ErroUfVisible
        {
            get => erroUfVisible;
            set { erroUfVisible = value; onPropertyChanged(); }
        }

        private bool erroCepVisible;
        public bool ErroCepVisible
        {
            get => erroCepVisible;
            set { erroCepVisible = value; onPropertyChanged(); }
        }

        private bool erroSenhaVisible;
        public bool ErroSenhaVisible
        {
            get => erroSenhaVisible;
            set { erroSenhaVisible = value; onPropertyChanged(); }
        }

        private bool erroNomeEmpresaVisible;
        public bool ErroNomeEmpresaVisible
        {
            get => erroNomeEmpresaVisible;
            set { erroNomeEmpresaVisible = value; onPropertyChanged(); }
        }

        private bool erroFotoVisible;
        public bool ErroFotoVisible
        {
            get => erroFotoVisible;
            set { erroFotoVisible = value; onPropertyChanged(); }
        }

        private bool erroBiografiaVisible;
        public bool ErroBiografiaVisible
        {
            get => erroBiografiaVisible;
            set { erroBiografiaVisible = value; onPropertyChanged(); }
        }

        private bool erroInstrumentosVisible;
        public bool ErroInstrumentosVisible
        {
            get => erroInstrumentosVisible;
            set { erroInstrumentosVisible = value; onPropertyChanged(); }
        }

        private bool erroGenerosVisible;
        public bool ErroGenerosVisible
        {
            get => erroGenerosVisible;
            set { erroGenerosVisible = value; onPropertyChanged(); }
        }

        private string empresa = string.Empty;
        public string Empresa
        {
            get { return empresa; }
            set
            {
                empresa = value;
                onPropertyChanged();
            }
        }
        private string nome = string.Empty;
        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                onPropertyChanged();
            }
        }

        private string email = string.Empty;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                onPropertyChanged();
            }
        }

        private string telefone = string.Empty;
        public string Telefone
        {
            get { return telefone; }
            set
            {
                telefone = value;
                onPropertyChanged();
            }
        }

        private string logradouro = string.Empty;
        public string Logradouro
        {
            get { return logradouro; }
            set
            {
                logradouro = value;
                onPropertyChanged();
            }
        }

        private string numero = string.Empty;
        public string Numero
        {
            get { return numero; }
            set
            {
                numero = value;
                onPropertyChanged();
            }
        }

        private string cep = string.Empty;
        public string Cep
        {
            get { return cep; }
            set
            {
                cep = value;
                onPropertyChanged();
            }
        }

        private string bairro = string.Empty;
        public string Bairro
        {
            get { return bairro; }
            set
            {
                bairro = value;
                onPropertyChanged();
            }
        }

        private string cidade = string.Empty;
        public string Cidade
        {
            get { return cidade; }
            set
            {
                cidade = value;
                onPropertyChanged();
            }
        }

        private string uf = string.Empty;
        public string Uf
        {
            get { return uf; }
            set
            {
                uf = value;
                onPropertyChanged();
            }
        }

        private string senha = string.Empty;
        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
                onPropertyChanged();
            }

        }

        private string codigoTelefone;
        public string CodigoTelefone
        {
            get => codigoTelefone;
            set
            {
                codigoTelefone = value;
                onPropertyChanged();
            }
        }





    }
}
