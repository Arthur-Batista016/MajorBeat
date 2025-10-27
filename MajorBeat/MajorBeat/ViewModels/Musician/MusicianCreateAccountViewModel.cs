using MajorBeat.Enums;
using MajorBeat.ModelsJeff;
using MajorBeat.ViewModels.Musician;
using MajorBeat.Views;
using MajorBeat.Views.Musicians;
using System.Windows.Input;

namespace MajorBeat.ViewModels
{
    public class MusicianCreateAccountViewModel : BaseViewModel
    {

        private readonly INavigation _navigation;
        private TipoMusico _tipo;
        public TipoMusico Tipo
        {
            get => _tipo;
            set
            {
                if (_tipo != value)
                {
                    _tipo = value;
                    onPropertyChanged(nameof(Tipo));       // atualiza o Picker
                    onPropertyChanged(nameof(NomeLabel)); // atualiza label
                }
            }
        }

        public MusicianCreateAccountViewModel(INavigation navigation)
        {
            _navigation = navigation;

        }




        // Label dinâmico
        public string NomeLabel => Tipo == TipoMusico.SOLO ? "Nome Completo" : "Nome da Banda";

        // Lista pro Picker
        public IEnumerable<TipoMusico> Tipos => Enum.GetValues(typeof(TipoMusico)).Cast<TipoMusico>();

        private bool ValidarCampos()
        {
            bool valido = true;

            if (string.IsNullOrWhiteSpace(Nome)) 
            { 
                ErroNomeVisible = true; 
                valido = false; 
            } else {
                ErroNomeVisible = false; 
            }
            if (string.IsNullOrWhiteSpace(Email) || !Email.EndsWith(".com"))
            { 
                ErroEmailVisible = true; 
                valido = false; 
            } else { 
                ErroEmailVisible = false;
            }
            if (string.IsNullOrWhiteSpace(Telefone) || Telefone.Length != 11 || !Telefone.All(char.IsDigit))
            { 
                ErroTelefoneVisible = true; 
                valido = false; 
            } else { 
                ErroTelefoneVisible = false; 
            }
            
            if (string.IsNullOrWhiteSpace(Numero) || !Numero.All(char.IsDigit)) 
            { 
                ErroNumeroVisible = true; 
                valido = false;
            } else { 
                ErroNumeroVisible = false;
            }
            if (string.IsNullOrWhiteSpace(Cep) || Cep.Length != 8 || !Cep.All(char.IsDigit)) {
                ErroCepVisible = true; 
                valido = false; 
            } else { 
                ErroCepVisible = false; 
            }
            if (string.IsNullOrWhiteSpace(Senha) || Senha.Length < 8) {
                ErroSenhaVisible = true; 
                valido = false;
            } else { 
                ErroSenhaVisible = false; 
            }


            return valido;
        }
        public async Task UserSave()
        {
            Musico u = new Musico();
            u.nome = Nome;
            u.email = Email;
            u.telefone = Telefone;
            u.endereco = $"{Cep}, {Numero}, {Complemento}";
            u.senha = Senha;
            u.tipoMusico = Tipo;


            if (ValidarCampos())
            {
                var viewmodel = new MusicianCreateProfileViewModel(u);
                await _navigation.PushAsync(new MusicianCreateProfileView(viewmodel));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Por favor, corrija os erros nos campos destacados.", "OK");
            }




            // Se passou na validação, avança para a próxima página

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
    }
}
