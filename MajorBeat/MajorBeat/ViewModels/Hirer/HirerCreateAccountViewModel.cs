using MajorBeat.ModelsJeff;
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


        public ICommand ProximoCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        private readonly INavigation _navigation;

       

        public HirerCreateAccountViewModel(INavigation navigation)
        {
            _navigation = navigation;
            InicializarCommands();
        }
        public void InicializarCommands()
        {
            ProximoCommand = new Command(async () => await UserSave());

        }
        public async Task UserSave()
        {
            Contratante u = new Contratante();
            u.nome = Nome;
            u.email = Email;
            u.telefone = Telefone;
            u.endereco = $"{Cep}, {Numero}, {Complemento}";
            u.senha = Senha;
            u.empresa = Empresa;



            if (ValidarCampos())
            {
                var viewmodel = new HirerCreateProfileViewModel(u);
                await _navigation.PushAsync(new HirerCreateProfileView(viewmodel));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Por favor, corrija os erros nos campos destacados.", "OK");
            }

        }


        private bool ValidarCampos()
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
            if (string.IsNullOrWhiteSpace(Numero) || !Numero.All(char.IsDigit))
            {
                ErroNumeroVisible = true;
                valido = false;
            }
            else
            {
                ErroNumeroVisible = false;
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

        private bool erroNumeroVisible;
        public bool ErroNumeroVisible
        {
            get => erroNumeroVisible;
            set { erroNumeroVisible = value; onPropertyChanged(); }
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

        private string complemento = string.Empty;
        public string Complemento
        {
            get { return complemento; }
            set
            {
                complemento = value;
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
