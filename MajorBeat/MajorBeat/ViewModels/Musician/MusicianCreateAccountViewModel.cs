using MajorBeat.Enums;
using MajorBeat.ModelsJeff;
using MajorBeat.Services.Users;
using MajorBeat.ViewModels.Musician;
using MajorBeat.Views;
using MajorBeat.Views.Musicians;
using System.Windows.Input;

namespace MajorBeat.ViewModels
{
    public class MusicianCreateAccountViewModel : BaseViewModel
    {
        private string _enderecoFormatadoValidado;
        private readonly CepService _cepService; 
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
        _cepService = new CepService();
        }




        // Label dinâmico
        public string NomeLabel => Tipo == TipoMusico.SOLO ? "Nome Completo" : "Nome da Banda";

        // Lista pro Picker
        public IEnumerable<TipoMusico> Tipos => Enum.GetValues(typeof(TipoMusico)).Cast<TipoMusico>();

        private async Task<bool> ValidarCampos()
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
            if (string.IsNullOrWhiteSpace(Senha) || Senha.Length < 8) {
                ErroSenhaVisible = true; 
                valido = false;
            } else { 
                ErroSenhaVisible = false; 
            }
            if (valido)
            {
                // Agora sim, fazemos a chamada à API
                string enderecoFormatado = await _cepService.BuscarEnderecoFormatadoAsync(Cep);


                // A VALIDAÇÃO CORRETA É ESTA:
                // Se o serviço retornou null, o CEP é inválido (não encontrado ou formato ruim).
                if (enderecoFormatado == null)
                {
                    ErroCepVisible = true;
                    valido = false; // Define a validação geral como falsa
                }
                else
                {

                    _enderecoFormatadoValidado = enderecoFormatado;

                    // Opcional: Se você quiser usar o endereço, ele está aqui.
                    // Ex: this.EnderecoCompleto = enderecoFormatado;
                    ErroCepVisible = false;
                }
            }
            else
            {
                // Se 'valido' já for falso (ex: Nome em branco), nem tentamos
                // checar o CEP, mas precisamos garantir que a msg de erro do CEP
                // não esteja aparecendo por engano de uma validação anterior.

                // Se o campo CEP estiver em branco, mostre o erro dele também.
                if (string.IsNullOrWhiteSpace(Cep))
                {
                    ErroCepVisible = true;
                    // 'valido' já é 'false', então não precisamos redefini-lo.
                }
            }


            return valido;
        }
        public async Task UserSave()
        {

            if (await ValidarCampos())
            {
                
                Musico u = new Musico();
                u.nome = Nome;
                u.email = Email;
                u.telefone = Telefone;
                if (string.IsNullOrWhiteSpace(Complemento))
                {

                    u.endereco = $"{_enderecoFormatadoValidado}, {Numero}";
                }
                else
                {
                u.endereco = $"{_enderecoFormatadoValidado}, {Numero}, {Complemento}";
                }
                u.senha = Senha;
                u.tipoMusico = Tipo;

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
